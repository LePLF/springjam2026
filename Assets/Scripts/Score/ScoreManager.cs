using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ScoreManager : MonoBehaviour
{
    [Header("Bloom Score")]
    public float scoreToEnd = 100f;
    public float scoreToLose = -100f;
    public static float currentScore;
    public UnityEvent onGainBloom;
    public UnityEvent onLoseBloom;

    [Header("Player Score")]
    public UnityEvent onPlayerGainScore;
    public UnityEvent onPlayerLoseScore;
    public int player1Score;
    public int player2Score;

    public List<GameObject> player1Kills = new List<GameObject>();
    public List<GameObject> player2Kills = new List<GameObject>();


    public List <int> player1KillValues = new List<int>();
    public List <int> player2KillValues = new List<int>();


    [Header("Game State")]
    public UnityEvent onGameLost;
    public UnityEvent onGameEnd;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1Kills.Clear();
        player2Kills.Clear();
        player1KillValues.Clear();
        player2KillValues.Clear();
        player1Score = 0;
        player2Score = 0;
        currentScore = 0;
    }

    public void ApplyBloom(int scoreValue)
    {
        print(scoreValue);
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

    public void ApplyPlayerScore(int playerIndex, int scoreValue, TargetableData scoreMouche)
    {
        AddEnemyToPlayerScoreArray(playerIndex, scoreMouche);
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

    public void AddEnemyToPlayerScoreArray(int playerIndex, TargetableData scoreMouche)
    {
        
        if (playerIndex > 0)
        {
            player2Kills.Add(scoreMouche.scoreMouche);
            player2KillValues.Add(scoreMouche.scoreValue);
        }
        else
        {
            player1Kills.Add(scoreMouche.scoreMouche);
            player1KillValues.Add(scoreMouche.scoreValue);
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
