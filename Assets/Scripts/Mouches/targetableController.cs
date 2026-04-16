using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class targetableController : MonoBehaviour
{
    public TargetableData CreatureData;

    private float moveSpeed;
    private float jitterSpeed;
    private float jitterRadius;

    private int bloomValue = 5;
    public UnityEvent OnDeath;
    public GameObject PathManager;
    private PointsList points;
    private Vector2 basePosition;
    private Vector2 jitterTarget;
    private Vector2 jitterOffset;

    public UnityEvent OnReachFlower;
    public GameObject scoreManager;
    private ScoreManager score;

    [NonSerialized]
    public AudioSource audioSource;

    private void Awake()
    {
        GetComponentInChildren<TargetableHealthManager>().creatureData = CreatureData;
    }

    void Start()
    {
        moveSpeed = CreatureData.moucheData.moveSpeed;
        jitterSpeed = CreatureData.moucheData.JitterSpeed;
        jitterRadius = CreatureData.moucheData.jitterRadius;
        bloomValue = CreatureData.moucheData.bloomValue;
        points = PathManager.GetComponent<PointsList>();
        score = scoreManager.GetComponent<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
        basePosition = transform.position;

        StartCoroutine(FollowPath());
        StartCoroutine(Jitter());
    }


    private IEnumerator FollowPath()
    {
        int currentList = 0;

        if (moveSpeed == 0 || points.path.Count == 0)
        {
            Debug.Log("movement speed is 0 / there is no path");
            yield break;
        }

        while (true)
        {
            Vector2 target = points.GetPathPointInList(currentList);

            while (Vector2.Distance(basePosition, target) > 0.1f)
            {
                basePosition = Vector2.MoveTowards(basePosition, target, moveSpeed * Time.deltaTime);
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

    private IEnumerator Jitter()
    {
        if (jitterRadius == 0 || jitterSpeed == 0)
        {
            yield break;
        }
        while (true)
        {
            jitterTarget = UnityEngine.Random.insideUnitCircle * jitterRadius;

            while (Vector2.Distance(jitterOffset, jitterTarget) > 0.05f)
            {
                jitterOffset = Vector2.MoveTowards(jitterOffset, jitterTarget, jitterSpeed * Time.deltaTime);
                yield return null;
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

    private void Update()
    {
        transform.position = basePosition + jitterOffset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(basePosition, jitterRadius);
    }


    public EffectType EffectType;

    

}
