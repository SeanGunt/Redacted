using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    void Start()
    {
        foreach (GameObject prefab in ObjectPool.instance.prefabsToPool)
        {
            ObjectPool.instance.InitiaizePool(ObjectPool.instance.prefabsToPool[1], 20, GameManager.Instance.poolHolders, 1);
        }
    }
}
