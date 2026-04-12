using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class targetableController : MonoBehaviour
{
    public TargetableData CreatureData;

    private float moveSpeed;

    private int bloomValue = 5;
    public UnityEvent OnDeath;
    public GameObject PathManager;
    private PointsList points;

    public UnityEvent OnReachFlower;
    public GameObject scoreManager;
    private ScoreManager score;

    private void Awake()
    {
        GetComponentInChildren<TargetableHealthManager>().creatureData = CreatureData;
    }

    void Start()
    {
        moveSpeed = CreatureData.moveSpeed;
        bloomValue = CreatureData.bloomValue;
        points = PathManager.GetComponent<PointsList>();
        score = scoreManager.GetComponent<ScoreManager>();

        StartCoroutine(FollowPath());
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

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }

    void ReachFlower()
    {
        score.ApplyBloom(bloomValue);
        Destroy(gameObject);
    }

}
