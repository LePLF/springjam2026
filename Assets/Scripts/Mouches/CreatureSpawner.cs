using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [Header("Refs")]
    public GameObject pathManager;
    public GameObject scoreManager;


    [Header("Spawned Entities")]
    public GameObject baseMoush;
    public TargetableData[] spawnList;

    [Header("Spawner Parameters")]
    public float spawnerBaseCooldown;
    public int maxSpawnedEntities;
    public bool isGameEnded;

    private int totalWeight;
    private GameObject instantiatedFly;
    private Coroutine spawnCoroutine;
    private float lastAttackTime;
    private float moushSpawnCooldown = 1f;

    public List<GameObject> activeFlyList = new List<GameObject>();

    public bool CooldownCheck(float cooldown)
    {
        return Time.time >= lastAttackTime + cooldown;
    }
    private void Awake()
    {
        SetSpawnWeights();
    }
    private void SetSpawnWeights()
    {
        totalWeight = 0;
        foreach (TargetableData moush in spawnList)
        {
            totalWeight += moush.spawnerData.moushSpawnWeight;
            moush.spawnerData.combinedWeight = totalWeight;
        }
    }
    private GameObject GetRandomMoush(ref float cooldown)
    {
        int random = Random.Range(0, totalWeight);
        foreach(TargetableData moush in spawnList)
        {
            
            if (random < moush.spawnerData.combinedWeight) return moush.spawnerData.moushPrefab;
        }

        return baseMoush;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(moushSpawnCooldown + spawnerBaseCooldown);

            activeFlyList.RemoveAll(c => c == null);

            if (maxSpawnedEntities > activeFlyList.Count)
            {
                instantiatedFly = Instantiate(GetRandomMoush(ref moushSpawnCooldown), transform.position, Quaternion.identity);

                instantiatedFly.GetComponent<targetableController>().PathManager = pathManager;
                instantiatedFly.GetComponent<targetableController>().scoreManager = scoreManager;
                instantiatedFly.GetComponentInChildren<TargetableHealthManager>().scoreManager = scoreManager;
                activeFlyList.Add(instantiatedFly);
            }         
        }
    }


    public void onGameEnd()
    {
        StopCoroutine(spawnCoroutine);
        isGameEnded = true;

        foreach (GameObject fly in activeFlyList)
        {
            if (fly!=null) Destroy(fly);
        }

        activeFlyList.Clear();
    }

    public void onGameLost()
    {
        StopCoroutine(spawnCoroutine);
        isGameEnded = true;
    }

   
    void Start()
    {
        isGameEnded = false;
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }


    void Update()
    {
        
    }
}
