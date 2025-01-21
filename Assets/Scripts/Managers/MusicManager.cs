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

    public void FadeTracks(AudioClip trackToFadeInto)
    {
        if (!currentlyFading)
        {
            StartCoroutine(FadeTracksTogether(trackToFadeInto));
        }
    }

    private IEnumerator FadeTracksTogether(AudioClip tracktoFadeInto)
    {
        currentlyFading = true;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= Time.deltaTime * 3f;
            yield return null;
        }
        musicSource.clip = tracktoFadeInto;
        musicSource.Play();
        while (musicSource.volume < 1f)
        {
            musicSource.volume += Time.deltaTime * 3f;
            yield return null;
        }
        currentlyFading = false;
    }
}
