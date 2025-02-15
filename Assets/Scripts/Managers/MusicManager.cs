using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioMixer mixer;
    public List<AudioClip> tracks;
    public AudioSource musicSource;
    private bool currentlyFading;

    private void Awake()
    {
        instance = this;
    }

    public void FadeTracks(AudioClip trackToFadeInto, float time)
    {
        if (!currentlyFading)
        {
            StartCoroutine(FadeTracksTogether(trackToFadeInto, time));
        }
    }

    public void SwapTracks(AudioClip trackToSwap)
    {
        musicSource.clip = trackToSwap;
        musicSource.Play();
    }

    private IEnumerator FadeTracksTogether(AudioClip tracktoFadeInto, float time)
    {
        currentlyFading = true;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= Time.deltaTime * 3f;
            yield return null;
        }
        musicSource.clip = tracktoFadeInto;
        musicSource.Play();
        musicSource.time = time;
        while (musicSource.volume < 1f)
        {
            musicSource.volume += Time.deltaTime * 3f;
            yield return null;
        }
        currentlyFading = false;
    }
}
