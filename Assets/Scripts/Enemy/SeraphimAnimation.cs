using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeraphimAnimation : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }
    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
