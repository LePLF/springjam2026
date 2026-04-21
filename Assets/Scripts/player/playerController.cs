using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    [Header("Refs")]
    public GameObject scoreManager;
    public ScoreManager score;
    private AudioSource audioSource;
    private Vector2 movement;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private float currentMoveSpeed;
    public bool isOnMenu;

    [Header("Movement Boundaries")]
    public Vector2 boundsCenter;
    public Vector2 boundsSize;
    private Vector2 MinBounds => new Vector2(boundsCenter.x - boundsSize.x / 2f, boundsCenter.y - boundsSize.y / 2f);
    private Vector2 MaxBounds => new Vector2(boundsCenter.x + boundsSize.x / 2f, boundsCenter.y + boundsSize.y / 2f);

    [Header("Index")]
    public int playerIndex;
    [SerializeField] private bool isPlayer2;
    public GameObject otherPlayer;

    [Header("Attack")]
    public int damageValue = 1;
    public float attackCooldown;
    [SerializeField] private float stunDuration;
    public Vector2 boxSize;
    public Vector2 boxPositionOffset;
    public LayerMask attackLayerMask;
    public AudioClip missSound;
    public AudioClip hitSound;
    public AudioClip enemyPlayerHitSound;
    private float lastAttackTime;
    private bool isStunned;
    [SerializeField] private Vector3 fixedBoxPosition;
    public UnityEvent onAttackMiss;
    public UnityEvent OnStun;

    [Header("Parry")]
    public float parryCooldown;
    public float parryDuration;
    [NonSerialized] public bool isParrying;
    private float lastParryTime;
    public AudioClip parryHitSound;
    
    [Header("Animators")]
    private Animator animator;
    private Animator Rotanimator;
    public GameObject sprite;
    public GameObject handle;   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isOnMenu == true ) return;
        score = scoreManager.GetComponent<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);        
    }

    void Start()
    {
        currentMoveSpeed = moveSpeed;
        animator = sprite.GetComponent<Animator>();
        Rotanimator = handle.GetComponent<Animator>();
        animator.SetBool("is2", isPlayer2);
        fixedBoxPosition = transform.position + (Vector3)boxPositionOffset;
    }

    public void StartAttack()
    {
        if (isStunned) return;
        if (!CooldownCheck(attackCooldown, lastAttackTime)) return;
        //print(playerIndex);
        onAttackMiss.Invoke();
        //print("is Attacking");
       
        Collider2D[] targets = Physics2D.OverlapBoxAll(fixedBoxPosition, boxSize,0,attackLayerMask);
        animator.SetTrigger("Hit");

        if (targets.Length == 0) audioSource.PlayOneShot(missSound);

        else audioSource.PlayOneShot(hitSound);

        foreach (Collider2D target in targets)
        {

            if (target.gameObject == otherPlayer)
            {
                if (target.GetComponent<playerController>().isParrying == true)
                {
                    audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                    audioSource.PlayOneShot(parryHitSound);
                    TriggerStun();
                    return;
                }
                target.GetComponent<playerController>().TriggerStun();
            }
            else
            {
                if (isOnMenu==true)
                {                  
                    target.GetComponent<MoovButton>().LoadSceneOnSwitch();
                }
                else
                {
                    if (target.gameObject.GetComponent<TargetableHealthManager>() == null)
                    {
                        target.gameObject.GetComponent<ImpulseOnSpawn>().DoImpulse();
                    }
                    else
                    target.gameObject.GetComponent<TargetableHealthManager>().TakeDamage(damageValue, playerIndex);
                }           
            }
        }
    }

    IEnumerator Stun()
    {
        isStunned = true;
        //print("ca stun");
        yield return new WaitForSeconds(stunDuration);
        //print("stun fini");
        isStunned = false;
    }

    public void TriggerStun()
    {
        if (!isStunned)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(enemyPlayerHitSound);
            OnStun.Invoke();
            CameraManager.Instance.ShakeLight();
            StartCoroutine(Stun());         
        }
    }

    public void Parry()
    {
        if (isStunned) return;
        if (isParrying) return;
        if (!CooldownCheck(parryCooldown, lastParryTime))
        {
            StartCoroutine(TriggerParry());
        }
    }

    public IEnumerator TriggerParry()
    {
        isParrying = true;
        yield return new WaitForSeconds(parryDuration);
        isParrying = false;

        yield break;
    }

    public bool CooldownCheck(float cooldown, float lastActionTime)
    {
        if (Time.time >= lastActionTime + cooldown)
        {
            lastActionTime = Time.time;
            return true;
        }
        return false;
    }

    void Update()
    {
        
        if (isStunned) return;
        
        if(isPlayer2)
        {
            movement.Set(playerinputManager.p2Movement.x, playerinputManager.p2Movement.y);
            animator.SetFloat("Vert", playerinputManager.p2MoveAction.ReadValue<Vector2>().y);
            Rotanimator.SetFloat("Horiz", playerinputManager.p2MoveAction.ReadValue<Vector2>().x);
        }
        else
        {
            movement.Set(playerinputManager.p1Movement.x, playerinputManager.p1Movement.y);
            animator.SetFloat("Vert", playerinputManager.p1MoveAction.ReadValue<Vector2>().y);
            Rotanimator.SetFloat("Horiz", playerinputManager.p1MoveAction.ReadValue<Vector2>().x);
        }

        rb.linearVelocity = movement * currentMoveSpeed;

        Vector2 clampedPos = new Vector2(Mathf.Clamp(transform.position.x, MinBounds.x, MaxBounds.x),Mathf.Clamp(transform.position.y, MinBounds.y, MaxBounds.y)
);
        transform.position = clampedPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        fixedBoxPosition = transform.position + (Vector3)boxPositionOffset;
        Gizmos.DrawWireCube(fixedBoxPosition, boxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boundsCenter, boundsSize);

    }
}
