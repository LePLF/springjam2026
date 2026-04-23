using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureData", menuName = "Data/CreatureData")]
public class TargetableData : ScriptableObject
{
    [Header("Mouche Parameters")]
    public MoucheData moucheData;

    [Header("Spawn Parameters")]
    public SpawnerData spawnerData;


    [Serializable]
    public class MoucheData
    {
        public int maxHealth;

        [Header("Movement")]
        public float moveSpeed;
        public float JitterSpeed;
        public float jitterRadius;
        public bool isUsingCurveForSpeed;
        public AnimationCurve speedModifier;

        [Header("Score")]
        public int scoreValue;
        public int bloomValue;
        public GameObject scoreMouche;


        [Header("Effects")]
        public ParticleSystem onDeathEffect;
        public ParticleSystem onHurtEffect;

        [Header("Sounds")]
        public DeathSound deathSound;
        public HurtSound hurtSound;

        [Serializable]
        public class DeathSound
        {
            public AudioClip sound;
            public float soundVolume;
            public float minPitch;
            public float maxPitch;
        }

        [Serializable]
        public class HurtSound
        {
            public AudioClip sound;
            public float soundVolume;
            public float minPitch;
            public float maxPitch;
        }
    }

    [Serializable]
    public class SpawnerData
    {
        public GameObject moushPrefab;
        public int moushSpawnWeight = 1;
        [NonSerialized] public int combinedWeight;
        public float cooldown = 1;
    }
    
    
    

}
