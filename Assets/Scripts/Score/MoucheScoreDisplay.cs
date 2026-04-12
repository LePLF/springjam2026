using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class MoucheScoreDisplay : MonoBehaviour
{
    public ScoreManager scoreManager;

    public float displaySpeed;
    public float timeToDisplayWinner;
    private bool isScoreDisplaying;

    private List<GameObject> player1Kills = new List<GameObject>();
    private List<GameObject> player2Kills = new List<GameObject>();

    public List<int> player1KillValues = new List<int>();
    public List<int> player2KillValues = new List<int>();

    public static int p1ScoreDisplay;
    public static int p2ScoreDisplay;

    private int p1;
    private int p2;

    [Header("MoucheDisplay Spawnpoint")]
    public Transform p1ScoreSpawnPoint;
    public Transform p2ScoreSpawnPoint;

    [Header("WinnerEvents")]
    public UnityEvent onPlayer1Win;
    public UnityEvent onPlayer2Win;
    public UnityEvent onDraw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void FindPlayerScore()
    {
        player1Kills = scoreManager.player1Kills;
        player2Kills = scoreManager.player2Kills;
        player1KillValues = scoreManager.player1KillValues;
        player2KillValues = scoreManager.player2KillValues;
        StartScoreCounting();
    }

    public void StartScoreCounting()
    {
        isScoreDisplaying = true;
        StartCoroutine(SpawnLoop());
    }


    private IEnumerator SpawnLoop()
    {

        while (isScoreDisplaying)
        {
            if (player1Kills.Count != 0)
            {
                if (p1 != player1Kills.Count)
                {
                    Instantiate(player1Kills[p1], p1ScoreSpawnPoint);

                    p1ScoreDisplay += player1KillValues[p1];
                }
            }
            
            yield return new WaitForSeconds(displaySpeed);

            if (player2Kills.Count != 0)
            {
                if (p2 != player2Kills.Count)
                {
                    Instantiate(player2Kills[p2], p2ScoreSpawnPoint);

                    p2ScoreDisplay += player2KillValues[p2];
                }
            }            

            p1++;
            p2++;

            if (p1 == player1Kills.Count || p2 == player2Kills.Count)
            {
                yield return new WaitForSeconds(displaySpeed);
                
                if(p1ScoreDisplay > p2ScoreDisplay)
                {
                    onPlayer1Win.Invoke();
                }
                else if (p1ScoreDisplay < p2ScoreDisplay)
                {
                    onPlayer2Win.Invoke();
                }
                else
                {
                    onDraw.Invoke();
                }

                    p1 = 0;
                p2 = 0;
                isScoreDisplaying = true;
                break;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {   
        
    }
}
