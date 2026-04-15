using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("Bloom Score")]
    public int scoreToEnd = 100;
    public int scoreToLose = -100;
    public int currentScore;
    public UnityEvent onGainBloom;
    public UnityEvent onLoseBloom;
    public GameObject flower;
    public Animator bloomAnimator;
    public Animator movementAnimator;

    [Header("Player Score")]
    public UnityEvent onPlayerGainScore;
    public UnityEvent onPlayerLoseScore;
    public int player1Score;
    public int player2Score;
    [NonSerialized] public List<GameObject> player1Kills = new List<GameObject>();
    [NonSerialized] public List<int> player1KillValues = new List<int>();
    [NonSerialized] public List<GameObject> player2Kills = new List<GameObject>();
    [NonSerialized] public List<int> player2KillValues = new List<int>();

    [Header("Game State")]
    public float timerToEndGame = 3;
    public UnityEvent onGameLost;
    public UnityEvent onGameEnd;

    [Header("Camera Move")]
    public GameObject cameraMoover;
    [SerializeField] private MooveCameraEnd mooveCameraEnd;

    void Start()
    {
        player1Kills.Clear();
        player2Kills.Clear();
        player1KillValues.Clear();
        player2KillValues.Clear();
        player1Score = 0;
        player2Score = 0;
        currentScore = 0;
        mooveCameraEnd = cameraMoover.GetComponent<MooveCameraEnd>();
    }

    public void ApplyBloom(int scoreValue)
    {
        currentScore += scoreValue;
        if (scoreValue < 0)
        {
            movementAnimator.SetTrigger("TakingDamage");
        }
        else
        {
            movementAnimator.SetTrigger("GainBloom");
        }


        if (currentScore >= scoreToEnd)
        {
            EndGame();
        }
        else if (currentScore <= scoreToLose)
        {
            GameLost();
        }
    }

    public void ApplyPlayerScore(int playerIndex, int scoreValue, TargetableData scoreMouche)
    {
        AddEnemyToPlayerScoreArray(playerIndex, scoreMouche, scoreValue);
        print("score++");
        if (playerIndex == 0)
        {
            player1Score += scoreValue;
        }
        else
        {
            player2Score += scoreValue;
        }

        if (scoreValue < 0)
        {
            onPlayerLoseScore.Invoke();
        }
        else
        {
            onPlayerGainScore.Invoke();
        }
    }

    private IEnumerator WinCinematic()
    {
        yield return new WaitForSeconds(timerToEndGame);
        onGameEnd.Invoke();
        mooveCameraEnd.MoveToB();
        yield break;
    }



    public void AddEnemyToPlayerScoreArray(int playerIndex, TargetableData scoreMouche, int scoreValue)
    {
        if (playerIndex > 0)
        {
            player2Kills.Add(scoreMouche.moucheData.scoreMouche);
            player2KillValues.Add(scoreMouche.moucheData.scoreValue);
        }
        else
        {
            player1Kills.Add(scoreMouche.moucheData.scoreMouche);
            player1KillValues.Add(scoreMouche.moucheData.scoreValue);
        }
    }

    void EndGame()
    {
        print("game end");
        StartCoroutine(WinCinematic());     
    }

    void GameLost()
    {
        print("game lost");
        onGameLost.Invoke();
    }

    void Update()
    {
        bloomAnimator.SetInteger("Bloom", currentScore);
    }
}
