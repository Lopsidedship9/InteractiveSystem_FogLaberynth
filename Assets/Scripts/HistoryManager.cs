using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public AudioSource historySource;
    public AudioSource ambientSource;
    public float delay = 1.0f;

    private bool isPlaying = false;
    private AudioClip historyClip;

    void Start()
    {
        if (historySource != null && historySource.clip != null)
        {
            historyClip = historySource.clip;
        }
        else
        {
            Debug.LogWarning("AudioSource o AudioClip no encontrado.");
        }

        if (ambientSource != null)
        {
            ambientSource.Stop();
        }
        else
        {
            Debug.LogWarning("No AudioSource component found on " + gameObject.name);
        }

        Invoke("PlaySound", delay);
    }

    void PlaySound()
    {
        if (historySource != null && historyClip != null)
        {
            historySource.Play();
            isPlaying = true;
            StartCoroutine(WaitForSoundToFinish());
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not set.");
        }
    }

    IEnumerator WaitForSoundToFinish()
    {
        while (isPlaying && historySource.isPlaying)
        {
            yield return null;
        }

        Debug.Log("AudioClip has finished playing.");
        ambientSource.Play();
    }

    void Update()
    {
        if (isPlaying && !historySource.isPlaying)
        {
            isPlaying = false;
        }
    }
}
