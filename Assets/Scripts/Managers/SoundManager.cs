using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip walkClip;
    public AudioClip stoneWallClip;
    public AudioClip explosionWoodClip;

    private AudioSource walkSource;
    private AudioSource stoneWallSource;
    private AudioSource explosionWoodSource;

    void Awake()
    {
        // Create AudioSources
        walkSource = gameObject.AddComponent<AudioSource>();
        stoneWallSource = gameObject.AddComponent<AudioSource>();
        explosionWoodSource = gameObject.AddComponent<AudioSource>();

        // Assign AudioClips to AudioSources
        walkSource.clip = walkClip;
        stoneWallSource.clip = stoneWallClip;
        explosionWoodSource.clip = explosionWoodClip;
    }

    public void PlayStoneWallSound()
    {
        stoneWallSource.loop = false;
        stoneWallSource.volume = 0.4f;
        stoneWallSource.Play();
    }

    public void StopStoneWallSound()
    {
        stoneWallSource.Stop();
    }

    public void PlayWalkingSound()
    {
        walkSource.loop = true;
        stoneWallSource.volume = 1.5f;
        walkSource.Play();
    }

    public void StopWalkingSound()
    {
        walkSource.Stop();
    }

    public void PlayExplosionWoodSound()
    {
        explosionWoodSource.loop = false;
        explosionWoodSource.volume = 0.5f;
        explosionWoodSource.Play();
    }

    public void StopExplosionWoodSound()
    {
        explosionWoodSource.Stop();
    }
}
