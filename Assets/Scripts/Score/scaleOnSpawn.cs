using System.Collections;
using UnityEngine;

public class scaleOnSpawn : MonoBehaviour
{
    [Header("Scale")]
    public AnimationCurve scaleCurve;
    public float popDuration = 0.4f;


    private void Start()
    {
        StartCoroutine(DoPop());
    }

    private IEnumerator DoPop()
    {
        float elapsed = 0f;
        Vector3 finalPosition = transform.position;

        while (elapsed < popDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / popDuration;

            float scale = scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        // Snap to exact final values
        transform.localScale = Vector3.one;
    }
}
