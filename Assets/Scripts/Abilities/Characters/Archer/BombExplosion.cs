using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionCirclePrefab;
    [SerializeField] private AudioClip explosionAudioClip;

    public void Explode()
    {
        Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
        SFXManager.instance.PlayOneShotAtPoint(transform.position, explosionAudioClip);
        Destroy(gameObject);
    }
}
