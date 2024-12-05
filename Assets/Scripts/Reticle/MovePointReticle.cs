using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointReticle : MonoBehaviour
{
    public void CreateReticle(Vector3 reticlePos)
    {
        GameObject reticle = ObjectPool.instance.GetPooledObject(ObjectPool.instance.listOfPooledObjects[0].prefabsToPool[0].prefab);
        reticle.transform.position = reticlePos;
        reticle.SetActive(true);
    }
}
