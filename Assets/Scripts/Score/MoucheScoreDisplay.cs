using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class MoucheScoreDisplay : MonoBehaviour
{
    public ScoreManager scoreManager;


    [Header("Display Parameters")]
    public AnimationCurve displayDelayCurve;
    public float timeToDisplayWinner = 5;
    public int maxScoreDisplaySpeed = 20;
    public AudioClip onDisplaySound;
    private bool isScoreDisplaying;
    private AudioSource onDisplaySoundPlayer;

    private List<GameObject> player1Kills = new List<GameObject>();
    private List<GameObject> player2Kills = new List<GameObject>();

    private List<int> player1KillValues = new List<int>();
    private List<int> player2KillValues = new List<int>();

    public static int p1ScoreDisplay;
    public static int p2ScoreDisplay;

    private int p1;
    private int p2;
    private bool isGameOver = false;

    [Header("MoucheDisplay Spawnpoint")]
    public Transform p1ScoreSpawnPoint;
    public Transform p2ScoreSpawnPoint;
    public float spawnRadius = 1f;


    [Header("ScoreTickUpEvent")]
    public UnityEvent onP1ScoreTickUp;
    public UnityEvent onP2ScoreTickUp;


    [Header("WinnerEvents")]
    public UnityEvent onPlayer1Win;
    public UnityEvent onPlayer2Win;
    public UnityEvent onDraw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onDisplaySoundPlayer = gameObject.GetComponent<AudioSource>();
        onDisplaySoundPlayer.pitch = Random.Range(0.9f, 1.1f);
        isGameOver = false;
    }


    public void FindPlayerScore()
    {
        player1Kills = scoreManager.player1Kills;
        player2Kills = scoreManager.player2Kills;
        player1KillValues = scoreManager.player1KillValues;
        player2KillValues = scoreManager.player2KillValues;
        if (isGameOver) return;
        StartScoreCounting();
        isGameOver = true;

    }

    public void StartScoreCounting()
    {
        isScoreDisplaying = true;
        StartCoroutine(SpawnLoop());
    }

    private Vector3 GetRandomSpawnOffset(Transform spawnPoint)
    {
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        return spawnPoint.position + (Vector3)randomOffset;
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(5);

        int maxKills = Mathf.Max(player1Kills.Count, player2Kills.Count);

        while (isScoreDisplaying)
        {
            float progress = (float)p1 / maxScoreDisplaySpeed;
            float displayDelay = displayDelayCurve.Evaluate(progress);

            if (player1Kills.Count != 0 && p1 < player1Kills.Count)
            {
                Instantiate(player1Kills[p1], GetRandomSpawnOffset(p1ScoreSpawnPoint), Quaternion.identity);
                p1ScoreDisplay += player1KillValues[p1];
                onP1ScoreTickUp.Invoke();
                onDisplaySoundPlayer.PlayOneShot(onDisplaySound);
            }

            if (player2Kills.Count != 0 && p2 < player2Kills.Count)
            {
                Instantiate(player2Kills[p2], p2ScoreSpawnPoint);
                p2ScoreDisplay += player2KillValues[p2];
                onP2ScoreTickUp.Invoke();
            }

            p1++;
            p2++;

            yield return new WaitForSeconds(displayDelay);

            if (p1 >= maxKills)
            {
                yield return new WaitForSeconds(timeToDisplayWinner);

                if (p1ScoreDisplay > p2ScoreDisplay) onPlayer1Win.Invoke();
                else if (p1ScoreDisplay < p2ScoreDisplay) onPlayer2Win.Invoke();
                else onDraw.Invoke();

                break;
            }
        }
    }
}
