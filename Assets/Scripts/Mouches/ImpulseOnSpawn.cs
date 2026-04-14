using UnityEngine;

public class ImpulseOnSpawn : MonoBehaviour
{
    [Header("Spawn Velocity")]
    public float minUpwardForce = 3f;
    public float maxUpwardForce = 6f;
    public float maxAngleForce = 3f;
    public float maxAngularVelocity = 100f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float upward = Random.Range(minUpwardForce, maxUpwardForce);
        float sideways = Random.Range(-maxAngleForce, maxAngleForce);

        rb.AddForce(new Vector2(sideways, upward), ForceMode2D.Impulse);
        rb.angularVelocity = Random.Range(-maxAngularVelocity, maxAngularVelocity);
    }
}
