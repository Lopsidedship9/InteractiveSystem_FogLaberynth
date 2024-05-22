using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip walkClip;
    public AudioClip stoneWallClip;

    private AudioSource walkSource;
    private AudioSource stoneWallSource;

    void Awake()
    {
        // Create AudioSources
        walkSource = gameObject.AddComponent<AudioSource>();
        stoneWallSource = gameObject.AddComponent<AudioSource>();

        // Assign AudioClips to AudioSources
        walkSource.clip = walkClip;
        stoneWallSource.clip = stoneWallClip;
    }

    public void PlayStoneWallSound()
    {
        stoneWallSource.loop = false;
        stoneWallSource.volume = 0.5f;
        stoneWallSource.Play();
    }

    public void StopStoneWallSound()
    {
        stoneWallSource.Stop();
    }

    public void PlayWalkingSound()
    {
        walkSource.loop = false;
        walkSource.Play();
    }

    public void StopWalkingSound()
    {
        walkSource.Stop();
    }
}
