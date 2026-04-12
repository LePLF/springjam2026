using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private float currentMoveSpeed;

    [SerializeField] private bool isPlayer2;

    public int playerIndex;
    public GameObject otherPlayer;

    [Header("Attack")]
    public int damageValue = 1;
    public LayerMask attackLayerMask;
    public Vector2 boxSize;
    public UnityEvent OnStun;
    public float attackCooldown;
    private float lastAttackTime;
    [SerializeField]private float stunDuration;
    private bool isStunned;

    public GameObject scoreManager;
    public ScoreManager score;
    public bool isOnMenu;
    //private MoovButtton buttonInstance;



    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isOnMenu == true ) return;
        score = scoreManager.GetComponent<ScoreManager>();
    }

    void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    public void StartAttack()
    {
        if (isStunned) return;
        if (!CooldownCheck(attackCooldown)) return;
        print(playerIndex);

        print("is Attacking");
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, boxSize,0,attackLayerMask);
        
        foreach(Collider2D target in targets)
        {

            if (target.gameObject == otherPlayer)
            {
                target.GetComponent<playerController>().TriggerStun();
            }
            else
            {
                print("mouche");
                if (isOnMenu==true)
                {
                    target.GetComponent<MoovButton>().LoadSceneOnSwitch();
                }
                else
                {
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
            OnStun.Invoke();
            CameraManager.Instance.ShakeLight();
            StartCoroutine(Stun());
            

        }

        
    }


    public bool CooldownCheck(float cooldown)
    {
        return Time.time >= lastAttackTime + cooldown;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (isStunned) return;
        if(isPlayer2)
        {
            movement.Set(playerinputManager.p2Movement.x, playerinputManager.p2Movement.y);
        }
        else
        {
            movement.Set(playerinputManager.p1Movement.x, playerinputManager.p1Movement.y);
        }

        rb.linearVelocity = movement * currentMoveSpeed;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
