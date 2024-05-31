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

        stoneWallSource.volume = 0.4f;
        stoneWallSource.volume = 2.0f;
        explosionWoodSource.volume = 0.5f;

        stoneWallSource.loop = false;
        walkSource.loop = true;
        explosionWoodSource.loop = false;
    }

    public void PlayStoneWallSound()
    {
        stoneWallSource.Play();
    }

    public void StopStoneWallSound()
    {
        stoneWallSource.Stop();
    }

    public void PlayWalkingSound()
    {
        walkSource.Play();
    }

    public void StopWalkingSound()
    {
        walkSource.Stop();
    }

    public void PlayExplosionWoodSound()
    {
        explosionWoodSource.Play();
    }

    public void StopExplosionWoodSound()
    {
        explosionWoodSource.Stop();
    }
}
