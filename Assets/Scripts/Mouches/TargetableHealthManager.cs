using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class TargetableHealthManager : MonoBehaviour
{
    public EMoucheID MoucheID;

    public TargetableData creatureData;
    public ParticleSystem onDeathEffect;

    private float moveSpeed;
    private float jitterSpeed;
    private float currentHealth;
    private bool hasPathEnded;

    public int scoreValue = 1;
    public int bloomValue = 5;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    public List<Transform> targets;
    public GameObject scoreManager;
    private ScoreManager score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasPathEnded = false;
        currentHealth = creatureData.maxHealth;
        moveSpeed = creatureData.moveSpeed;
        jitterSpeed = creatureData.JitterSpeed;
        score = scoreManager.GetComponent<ScoreManager>();

        StartCoroutine(MoveToRandomPoint());
    }

    public void TakeDamage(float damage, int playerIndex)
    {
        print(score);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {

            print("ouille");
            score.ApplyPlayerScore(playerIndex, scoreValue);

            //Instantiate(onDeathEffect,transform.position, Quaternion.identity);
            OnDeath.Invoke();
        }
        else OnTakeDamage.Invoke();
    }
    private IEnumerator MoveToRandomPoint()
    {
        while (true)
        {
            int chosenPoint = Random.Range(0, targets.Count);
            Vector2 target = targets[chosenPoint].position;

            while (Vector2.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, jitterSpeed * Time.deltaTime);
                yield return null;
            }
            if (hasPathEnded)
            {
                yield break;
            }
        }
    }

    public void ReachPathEnd()
    {
        hasPathEnded = true;
       
    }

}
