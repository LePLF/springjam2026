using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [Header("Spawned Entities")]
    public GameObject baseMoush;
    public MoushSpawnData[] spawnList;

    [Header("Spawner Parameters")]
    public float spawnerCooldown;
    private float moushSpawnCooldown = 1f;
    public int maxSpawnedEntities;
    private float lastAttackTime;
    public bool isGameEnded;
    private int totalWeight;

    public bool CooldownCheck(float cooldown)
    {
        return Time.time >= lastAttackTime + cooldown;
    }

    private void SetSpawnWeights()
    {
        totalWeight = 0;
        foreach (MoushSpawnData moush in spawnList)
        {
            totalWeight += moush.moushSpawnWeight;
            moush.combinedWeight = totalWeight;
        }
    }
    private GameObject GetRandomMoush(ref float cooldown)
    {
        int random = Random.Range(0, totalWeight);
        foreach(MoushSpawnData moush in spawnList)
        {
            if (random < moush.combinedWeight) return moush.moushPrefab;
        }

        return baseMoush;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(moushSpawnCooldown);

           Instantiate(GetRandomMoush(ref moushSpawnCooldown));

            if (isGameEnded)
            {         
                yield break;
            }
        }
    }
   
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }


    void Update()
    {
        
    }
}
