using UnityEngine;
using UnityEngine.Events;


public class ScoreManager : MonoBehaviour
{
    [Header("Bloom Score")]
    public float scoreToEnd = 100f;
    public float scoreToLose = -100f;
    private float currentScore;
    public UnityEvent onGainBloom;
    public UnityEvent onLoseBloom;

    [Header("Player Score")]
    public UnityEvent onPlayerGainScore;
    public UnityEvent onPlayerLoseScore;
    public int player1Score;
    public int player2Score;


    [Header("Game State")]
    public UnityEvent onGameLost;
    public UnityEvent onGameEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1Score = 0;
        player2Score = 0;
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

    public void ApplyPlayerScore(int playerIndex, int scoreValue)
    {
        print("score++");
        if (playerIndex == 0)
        {
            player1Score += scoreValue;
        }
        else
        {
            player2Score += scoreValue;
        }

        if (scoreValue <0)
        {
            onPlayerLoseScore.Invoke();
        }
        else
        {
            onPlayerGainScore.Invoke();
        }

    }


    void EndGame()
    {
        print("game end");
        onGameEnd.Invoke();
    }

    void GameLost()
    {
        print("game lost");
        onGameLost.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
