using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float pitchMin = 0.9f;
    [SerializeField] float pitchMax = 1.1f;
    [SerializeField] float volumeMin = 0.8f;
    [SerializeField] float volumeMax = 1.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayWithRandomPitch();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayWithRandomPitch();
        }
    }

    void PlayWithRandomPitch()
    {
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.volume = Random.Range(volumeMin, volumeMax);
        audioSource.Play();
    }
}
