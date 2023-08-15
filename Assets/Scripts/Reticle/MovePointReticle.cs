using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointReticle : MonoBehaviour
{
    public void CreateReticle(Vector3 reticlePos)
    {
        GameObject reticle = ObjectPool.instance.GetPooledObjects();
        if (reticle != null)
        {
            reticle.transform.position = reticlePos;
            reticle.SetActive(true);
        }
    }
}
