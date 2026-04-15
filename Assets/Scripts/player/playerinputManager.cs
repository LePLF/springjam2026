using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class playerinputManager : MonoBehaviour
{
    [Header("Movement Values")]
    public static Vector2 p1Movement;
    public static Vector2 p2Movement;
    public static InputAction p1MoveAction;
    public static InputAction p2MoveAction;

    [Header("Attack")]
    public UnityEvent player1Attack;
    public UnityEvent player2Attack;

    private PlayerInput playerInput;

    private InputAction p1AttackAction;
    private InputAction p2AttackAction;

    [Header("Parry")]
    public UnityEvent player1Parry;
    public UnityEvent player2Parry;


    private InputAction p1ParryAction;
    private InputAction p2ParryAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        p1MoveAction = playerInput.actions["Player1Move"];
        p2MoveAction = playerInput.actions["Player2Move"];

        p1AttackAction = playerInput.actions["Player1Attack"];  
        p2AttackAction = playerInput.actions["Player2Attack"];

        p1ParryAction = playerInput.actions["Player1Parry"];
        p2ParryAction = playerInput.actions["Player2Parry"];




    }

    void Start()
    {
        
    }


    void PlayerAttack()
    {
        if (p1AttackAction.WasPressedThisFrame())
        {
            player1Attack.Invoke();
        }
        else if (p2AttackAction.WasPressedThisFrame())
        {
            player2Attack.Invoke();
        }
    }
    
    void PlayerParry()
    {
        if (p1ParryAction.WasPressedThisFrame())
        {
            player1Parry.Invoke();
        }
        if (p2ParryAction.WasPressedThisFrame())
        {
            player2Parry.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        p1Movement = p1MoveAction.ReadValue<Vector2>();
        p2Movement = p2MoveAction.ReadValue<Vector2>();
        
        PlayerAttack();
        PlayerParry();
    }
}
