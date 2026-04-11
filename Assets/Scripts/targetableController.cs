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

        StartCoroutine(FollowPath());
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
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

            // Move toward the target until close enough
            while (Vector2.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Advance to the next list, loop back to 0 at the end
            currentList = (currentList + 1) % points.GetPathCount();
            if (currentList == points.GetPathCount())
            {
                OnReachFlower.Invoke();
            }
        }
    }

    void ReachFlower()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
