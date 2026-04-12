using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmpP1;
    [SerializeField] private TextMeshPro tmpP2;

    private void Update()
    {
        tmpP1.text = "P1 : " + MoucheScoreDisplay.p1ScoreDisplay;
        tmpP2.text = "P2 : " + MoucheScoreDisplay.p1ScoreDisplay;
    }
}
