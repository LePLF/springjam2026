using UnityEngine;
using Unity.Cinemachine;

public class ShakeListener : MonoBehaviour
{
    public CinemachineImpulseSource impulse;

    public void Shake()
    {
        impulse.GenerateImpulse();
    }
} 

