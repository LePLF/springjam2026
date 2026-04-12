using UnityEngine;
using UnityEngine.InputSystem;
public class MooveCameraEnd : MonoBehaviour
{
    [SerializeField] private Vector2   pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float     moveSpeed = 5f;

    private Vector2 _target;

    private void Start()
    {
        _target = pointA;
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
            _target = _target == pointA ? (Vector2)pointB.position : pointA;

        transform.position = Vector2.MoveTowards(transform.position, _target, moveSpeed * Time.deltaTime);
    }
}
