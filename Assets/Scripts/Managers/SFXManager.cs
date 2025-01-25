using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private GameObject soundObject;
    [SerializeField] private Transform soundHolder;

    private void Awake()
    {
        instance = this;
    }

    public void PlayOneShotAtPoint(Vector3 positionToSpawn, AudioClip clip)
    {
        StartCoroutine(Play(positionToSpawn, clip));
    } 
    public IEnumerator Play(Vector3 positionToSpawn, AudioClip clip)
    {
        GameObject spawnedSoundObect = Instantiate(soundObject, positionToSpawn, Quaternion.identity, soundHolder);
        AudioSource audioSource = spawnedSoundObect.GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Destroy(spawnedSoundObect);
    }
}
