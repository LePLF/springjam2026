using UnityEngine;

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
        transform.position = Vector2.MoveTowards(transform.position, _target, moveSpeed * Time.deltaTime);
    }

    public void MoveToB()
    {
        _target = pointB.position;
    }

    public void MoveToA()
    {
        _target = pointA;
    }
}