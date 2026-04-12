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
    public MoushSpawnData[] spawnList;

    [Header("Spawner Parameters")]
    public float spawnerBaseCooldown;
    private float moushSpawnCooldown = 1f;
    public int maxSpawnedEntities;
    private float lastAttackTime;
    public bool isGameEnded;
    private int totalWeight;
    private GameObject instantiatedFly;
    private Coroutine spawnCoroutine;

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
