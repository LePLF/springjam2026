using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class playerinputManager : MonoBehaviour
{
    public static Vector2 p1Movement;
    public static Vector2 p2Movement;

    public UnityEvent player1Attack;
    public UnityEvent player2Attack;

    private PlayerInput playerInput;

    private InputAction p1MoveAction;
    private InputAction p2MoveAction;

    private InputAction p1AttackAction;
    private InputAction p2AttackAction;





    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        p1MoveAction = playerInput.actions["Player1Move"];
        p2MoveAction = playerInput.actions["Player2Move"];

        p1AttackAction = playerInput.actions["Player1Attack"];  
        p2AttackAction = playerInput.actions["Player2Attack"];



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
        

    // Update is called once per frame
    void Update()
    {
        p1Movement = p1MoveAction.ReadValue<Vector2>();
        p2Movement = p2MoveAction.ReadValue<Vector2>();

        PlayerAttack();
    }
}
