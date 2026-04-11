using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class targetableController : MonoBehaviour
{
    public float health = 1f;
    public float moveSpeed = 1f;
    private float currentHealth;

    public int scoreValue = 1;
    public int bloomValue = 5;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;
    public GameObject PathManager;
    private PointsList points;

    public UnityEvent OnReachFlower;
    public GameObject scoreManager;
    private ScoreManager score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = PathManager.GetComponent<PointsList>();
        currentHealth = health;
        score = scoreManager.GetComponent<ScoreManager>();

        StartCoroutine(FollowPath());
    }

    public void TakeDamage(float damage, int playerIndex)
    {
        print("ouille");
        health -= damage;
        if (health <= 0)
        {
            score.ApplyPlayerScore(playerIndex, scoreValue);
            OnDeath.Invoke();
        }
        else OnTakeDamage.Invoke();
    }
    private IEnumerator FollowPath()
    {
        int currentList = 0;

        while (true)
        {
            Vector2 target = points.GetPathPointInList(currentList);

            while (Vector2.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentList++;

            if (currentList >= points.GetPathCount())
            {
                OnReachFlower.Invoke();
                ReachFlower();
                yield break;
            }
        }
    }

    void ReachFlower()
    {
        score.ApplyScore(scoreValue);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
