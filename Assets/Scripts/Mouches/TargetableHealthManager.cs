using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class TargetableHealthManager : MonoBehaviour
{
    public EMoucheID MoucheID;

    [NonSerialized] 
    public TargetableData creatureData;

    private ParticleSystem onDeathEffect;
    private targetableController targetableController;

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

    private AudioClip deathSound;
    private AudioClip hurtSound;


    [Header("Events")]
    public UnityEvent onMoushHit;
    public UnityEvent onBeeHit;

    void Start()
    {
        score = scoreManager.GetComponent<ScoreManager>();
        targetableController = transform.parent.GetComponent<targetableController>();
        hasPathEnded = false;
        currentHealth = creatureData.moucheData.maxHealth;
        jitterSpeed = creatureData.moucheData.JitterSpeed;
        onDeathEffect = creatureData.moucheData.onDeathEffect;
        hurtSound = creatureData.moucheData.hurtSound.sound;
        deathSound = creatureData.moucheData.deathSound.sound;

        //StartCoroutine(MoveToRandomPoint());
    }

    public void TakeDamage(float damage, int playerIndex)
    {
        //print(score);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlaySound(EffectType.Death);
            PlayEffect(EffectType.Death);

            if (creatureData.moucheData.scoreValue > 0)
            {
                onBeeHit.Invoke();
            }
            else
            {
                onMoushHit.Invoke();
            }

            //print("Player " + playerIndex);
            //print("Data  " + creatureData.scoreMouche.name);

            score.ApplyPlayerScore(playerIndex, creatureData.moucheData.scoreValue, creatureData);

            OnDeath.Invoke();
            targetableController.DestroyEntity();
        }
        else
        {
            PlayEffect(EffectType.Hurt);
            PlaySound(EffectType.Hurt);
            OnTakeDamage.Invoke();
        }
    }
    /*private IEnumerator MoveToRandomPoint()
    {
        while (true)
        {
            int chosenPoint = UnityEngine.Random.Range(0, targets.Count);
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
    }*/

    public void ReachPathEnd()
    {
        hasPathEnded = true;
       
    }

    public void onGameEnd()
    {
        Destroy(gameObject);
    }

    public void PlayEffect(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.Death:

                if (creatureData.moucheData.onDeathEffect == null) break;
                ParticleSystem effect = Instantiate(creatureData.moucheData.onDeathEffect, transform.position, Quaternion.identity);
                var main = effect.main;
                main.stopAction = ParticleSystemStopAction.Destroy;

                effect.Play();
                break;

            case EffectType.Hurt:

                if (creatureData.moucheData.onHurtEffect == null) break;
                ParticleSystem effect1 = Instantiate(creatureData.moucheData.onHurtEffect, transform.position, Quaternion.identity);
                var main1 = effect1.main;
                main1.stopAction = ParticleSystemStopAction.Destroy;

                effect1.Play();
                break;
        }
    }

    public void PlaySound(EffectType effectType)
    {
        Debug.Log("PlaySound called with: " + effectType);
        Debug.Log("AudioSource: " + targetableController.audioSource);
        Debug.Log("onDeathSound: " + creatureData.moucheData.deathSound.sound);
        Debug.Log("onHurtSound: " + creatureData.moucheData.hurtSound.sound);

        switch (effectType)
        {
            case EffectType.Death:
                if (deathSound == null) break;
                print("sound is playing");
                Utilities.PlayClipAtPointWithPitch(deathSound, transform.position, creatureData.moucheData.deathSound.soundVolume, creatureData.moucheData.deathSound.minPitch, creatureData.moucheData.deathSound.maxPitch);
                break;

            case EffectType.Hurt:
                if (hurtSound == null) break;
                Utilities.PlayClipAtPointWithPitch(hurtSound, transform.position, creatureData.moucheData.hurtSound.soundVolume, creatureData.moucheData.hurtSound.minPitch, creatureData.moucheData.hurtSound.maxPitch);
                break;
        }
    }

}
