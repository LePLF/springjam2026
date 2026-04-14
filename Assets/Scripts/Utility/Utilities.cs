using UnityEngine;

public static class Utilities
{
    public static void PlayClipAtPointWithPitch(AudioClip clip, Vector3 position, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioUtils: clip is null");
            return;
        }

        GameObject tempAudio = new GameObject("TempAudio");
        tempAudio.transform.position = position;
        AudioSource source = tempAudio.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.spatialBlend = 0f;
        source.Play();
        Object.Destroy(tempAudio, clip.length / source.pitch);
    }
}
