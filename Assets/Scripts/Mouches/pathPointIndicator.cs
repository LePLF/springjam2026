using UnityEngine;

public class pathPointIndicator : MonoBehaviour
{
    public float indicatorSize;
    public Color gizmoColor;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, indicatorSize);
    }
}
