using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource backgroundAudioSource;
    public List<AudioClip> tracks;
    private bool currentlyFading;

    private void Awake()
    {
        instance = this;
    }

    public void FadeTracks(AudioClip trackToFadeInto)
    {
        if (!currentlyFading)
        {
            StartCoroutine(FadeTracksTogether(trackToFadeInto));
        }
    }

    private IEnumerator FadeTracksTogether(AudioClip tracktoFadeInto)
    {
        float currentVolume = backgroundAudioSource.volume;
        currentlyFading = true;
        while (backgroundAudioSource.volume > 0)
        {
            backgroundAudioSource.volume -= Time.deltaTime;
            yield return null;
        }
        backgroundAudioSource.clip = tracktoFadeInto;
        backgroundAudioSource.Play();
        while (backgroundAudioSource.volume <= currentVolume)
        {
            backgroundAudioSource.volume += Time.deltaTime;
            yield return null;
        }
        currentlyFading = false;
    }
}
