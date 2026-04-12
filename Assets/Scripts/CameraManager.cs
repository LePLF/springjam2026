using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [Header(" CinemachineCamera type ")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [Header("Shake ")]
    [SerializeField] private float lightAmplitude = 0.5f;
    [SerializeField] private float lightDuration   = 0.15f;

    

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void ShakeLight()              => Shake(lightAmplitude, lightDuration);
    public void ShakeCustom(float a, float d) => Shake(a, d);

    private void Shake(float amplitude, float duration)
    {
        if (impulseSource == null) return;
        impulseSource.ImpulseDefinition.ImpulseDuration = duration;
        impulseSource.GenerateImpulse(amplitude);
    }
}
