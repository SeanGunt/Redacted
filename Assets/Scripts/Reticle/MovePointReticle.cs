using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointReticle : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;
    private GameObject reticle;
    public void CreateReticle(Vector3 reticlePos)
    {
        reticle = Instantiate(reticlePrefab, reticlePos, Quaternion.identity);
    }
}
