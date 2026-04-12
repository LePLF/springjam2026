using UnityEngine;

[CreateAssetMenu(fileName = "CreatureData", menuName = "Data/CreatureData")]
public class TargetableData : ScriptableObject
{
    public int maxHealth;

    [Header("Movement")]
    public float moveSpeed;
    public int JitterSpeed;

    [Header("Score")]
    public int scoreValue;
    public int bloomValue;
    public GameObject scoreMouche;

}
