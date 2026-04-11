using UnityEngine;
using UnityEngine.UI;

public class WobbleButton : MonoBehaviour
{
    [Header("Mouvement")]
    public float speed = 2f;        // Vitesse de déplacement
    public float range = 100f;      // Amplitude du mouvement (pixels)

    private Vector2 startPos;
    private Vector2 targetPos;
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        startPos = rt.anchoredPosition;
        PickNewTarget();
    }

    void Update()
    {
        rt.anchoredPosition = Vector2.MoveTowards(
            rt.anchoredPosition, targetPos, speed * Time.deltaTime * 60f
        );

        if (Vector2.Distance(rt.anchoredPosition, targetPos) < 5f)
            PickNewTarget();
    }

    void PickNewTarget()
    {
        targetPos = startPos + new Vector2(
            Random.Range(-range, range),
            Random.Range(-range, range)
        );
    }
}
