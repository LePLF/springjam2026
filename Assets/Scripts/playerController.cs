using UnityEngine;
using UnityEngine.Events;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private bool isPlayer2;

    public int playerIndex;
    public GameObject otherPlayer;

    [Header("Attack")]
    public LayerMask attackLayerMask;
    public Vector2 boxSize;
    public UnityEvent OnStun;

    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    public void StartAttack()
    {
        print("is Attacking");
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, boxSize,0,attackLayerMask);
        
        foreach(Collider2D target in targets)
        {
            if (target.gameObject == otherPlayer)
            {
                print(playerIndex);
                print(target.GetComponent<playerController>().playerIndex);
            }
            else
            {
                print("mouche ou abeille");
            }
        }
    }

    public void Stun()
    {
        OnStun.Invoke();
        //movementSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if(isPlayer2)
        {
            movement.Set(playerinputManager.p2Movement.x, playerinputManager.p2Movement.y);
        }
        else
        {
            movement.Set(playerinputManager.p1Movement.x, playerinputManager.p1Movement.y);
        }

        rb.linearVelocity = movement * moveSpeed;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
