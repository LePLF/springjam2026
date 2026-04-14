using System.Collections;
using UnityEngine;

public class ScorePunch : MonoBehaviour
{
    public AnimationCurve punchCurve;
    public float punchScale = 1.3f;
    public float punchDuration = 0.3f;
    public float maxRotation = 5f;

    private RectTransform rectTransform;
    private Coroutine currentPunch;
    private float currentScale = 1f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Punch()
    {
        if (currentPunch != null)
            StopCoroutine(currentPunch);
        currentPunch = StartCoroutine(DoPunch());
    }

    private IEnumerator DoPunch()
    {
        float startScale = currentScale;
        float randomRotation = Random.Range(-maxRotation, maxRotation);
        float elapsed = 0f;

        while (elapsed < punchDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / punchDuration;

            // Scale: punch up from current scale then back to 1
            currentScale = Mathf.LerpUnclamped(punchScale, 1f, punchCurve.Evaluate(t));
            rectTransform.localScale = Vector3.one * currentScale;

            // Rotation: wobble then back to 0
            float rotation = Mathf.LerpUnclamped(randomRotation, 0f, punchCurve.Evaluate(t));
            rectTransform.localEulerAngles = new Vector3(0f, 0f, rotation);

            yield return null;
        }

        currentScale = 1f;
        rectTransform.localScale = Vector3.one;
        rectTransform.localEulerAngles = Vector3.zero;
        currentPunch = null;
    }
}
