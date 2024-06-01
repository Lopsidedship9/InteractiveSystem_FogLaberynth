using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip walkClip;
    public AudioClip stoneWallClip;
    public AudioClip explosionWoodClip;

    private AudioSource walkSource;
    private AudioSource explosionWoodSource;
    private AudioSource[] stoneWallSources;

    private const int stoneWallChannels = 4;
    private Dictionary<int, float> stoneWallStartTimes;

    void Awake()
    {
        // Create AudioSources
        walkSource = gameObject.AddComponent<AudioSource>();
        explosionWoodSource = gameObject.AddComponent<AudioSource>();

        // Create an array for stone wall sources
        stoneWallSources = new AudioSource[stoneWallChannels];
        stoneWallStartTimes = new Dictionary<int, float>();

        for (int i = 0; i < stoneWallChannels; i++)
        {
            stoneWallSources[i] = gameObject.AddComponent<AudioSource>();
            stoneWallSources[i].clip = stoneWallClip;
            stoneWallSources[i].volume = 0.4f;
            stoneWallSources[i].loop = false;
            stoneWallStartTimes[i] = Mathf.Infinity;
        }

        // Assign AudioClips to AudioSources
        walkSource.clip = walkClip;
        explosionWoodSource.clip = explosionWoodClip;

        walkSource.volume = 2.0f;
        explosionWoodSource.volume = 0.5f;

        walkSource.loop = true;
        explosionWoodSource.loop = false;
    }

    public void PlayStoneWallSound()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!IsStoneWallSoundPlaying(i))
            {
                stoneWallSources[i].Play();
                stoneWallStartTimes[i] = Time.time;
                break;
            }
        }
    }

    public bool IsStoneWallSoundPlaying(int channel)
    {
        if (channel >= 0 && channel < stoneWallChannels)
        {
            return stoneWallSources[channel].isPlaying;
        }
        return true;
    }

    public void StopStoneWallSound()
    {
        int earliestChannel = GetEarliestPlayingStoneWallChannel();
        if (earliestChannel != -1)
        {
            stoneWallSources[earliestChannel].Stop();
            stoneWallStartTimes[earliestChannel] = Mathf.Infinity; // Reset the start time
        }
    }

    public int GetEarliestPlayingStoneWallChannel()
    {
        int earliestChannel = -1;
        float earliestTime = Mathf.Infinity;

        for (int i = 0; i < stoneWallChannels; i++)
        {
            if (stoneWallSources[i].isPlaying && stoneWallStartTimes[i] < earliestTime)
            {
                earliestTime = stoneWallStartTimes[i];
                earliestChannel = i;
            }
        }

        return earliestChannel;
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
