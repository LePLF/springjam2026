using UnityEngine;

[CreateAssetMenu(fileName = "MoushSpawnData", menuName = "Scriptable Objects/MoushSpawnData")]
public class MoushSpawnData : ScriptableObject
{
    public GameObject moushPrefab;
    public int moushSpawnWeight = 1;
    public int combinedWeight;
    public float cooldown = 1;
}