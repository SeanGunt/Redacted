using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    void Start()
    {
        foreach (GameObject prefab in ObjectPool.instance.prefabsToPool)
        {
            ObjectPool.instance.InitiaizePool(ObjectPool.instance.prefabsToPool[2], 1000, GameManager.Instance.poolHolders, 2);
        }
    }
}
