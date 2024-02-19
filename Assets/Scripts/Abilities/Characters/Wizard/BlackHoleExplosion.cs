using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionCirclePrefab;

    public void Explode()
    {
        Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
        Destroy(gameObject);
    }
}
