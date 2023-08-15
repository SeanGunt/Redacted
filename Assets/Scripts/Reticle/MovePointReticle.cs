using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointReticle : MonoBehaviour
{

    private void Start()
    {
        foreach (GameObject prefab in ObjectPool.instance.prefabsToPool)
        {
            ObjectPool.instance.InitiaizePool(ObjectPool.instance.prefabsToPool[0], 3, GameManager.Instance.poolHolders, 0);
        }
    }
    public void CreateReticle(Vector3 reticlePos)
    {
        GameObject reticle = ObjectPool.instance.GetPooledObject(ObjectPool.instance.prefabsToPool[0]);
        if (reticle != null)
        {
            reticle.transform.position = reticlePos;
            reticle.SetActive(true);
        }
    }
}
