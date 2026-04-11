using UnityEngine;
using UnityEngine.Events;


public class ScoreManager : MonoBehaviour
{
    public float scoreToEnd = 100f;
    public float scoreToLose = -100f;
    private float currentScore;
    public UnityEvent onGainBloom;
    public UnityEvent onLoseBloom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScore = scoreToEnd;
    }

    public void ApplyScore(int scoreValue)
    {
        if (scoreValue < 0)
        {
            currentScore -= scoreValue;
            onLoseBloom.Invoke();
        }
        else
        {
            currentScore += scoreValue;
            onGainBloom.Invoke();
        }
        if (scoreValue >= scoreToEnd)
        {
            EndGame();
        }
        else if (scoreValue <= scoreToLose)
        {
            GameLost();
        }
    }

    void EndGame()
    {

    }

    void GameLost()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
