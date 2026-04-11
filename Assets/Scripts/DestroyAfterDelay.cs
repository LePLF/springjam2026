using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ApplyDelay());
    }
    private IEnumerator ApplyDelay()
    {
        yield return new WaitForSeconds(delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
