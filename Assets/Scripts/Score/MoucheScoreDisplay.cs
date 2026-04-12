using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoucheScoreDisplay : MonoBehaviour
{
    public ScoreManager scoreManager;

    public float displaySpeed;
    private bool isScoreDisplaying;

    private List<GameObject> player1Kills = new List<GameObject>();
    private List<GameObject> player2Kills = new List<GameObject>();
    private int p1;
    private int p2;


    public Transform p1ScoreSpawnPoint;
    public Transform p2ScoreSpawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public void FindPlayerScore()
    {
        player1Kills = scoreManager.player1Kills;
        player2Kills = scoreManager.player2Kills;
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

            if (p1 != player1Kills.Count)
            {
                Instantiate(player1Kills[p1], p1ScoreSpawnPoint);
            }

            yield return new WaitForSeconds(displaySpeed);

            if (p2 != player2Kills.Count)
            {
                Instantiate(player2Kills[p2], p2ScoreSpawnPoint);
            }

            p1++;
            p2++;

            if (p1 == player1Kills.Count || p2 == player2Kills.Count)
            {
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
