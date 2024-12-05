using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PooledObjectsInfo
{
    public GameObject prefab;
    public float amountToPool;
}

[System.Serializable]
public class PooledObjects
{
    public List<PooledObjectsInfo> prefabsToPool = new List<PooledObjectsInfo>();
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public List<PooledObjects> listOfPooledObjects;
    private Dictionary<GameObject, List<GameObject>> pooledObjectsDict = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitiaizePool();
    }

    public void InitiaizePool()
    {
        for (int i = 0; i < listOfPooledObjects.Count; i++)
        {
            for (int j = 0; j < listOfPooledObjects[i].prefabsToPool.Count; j++)
            {
                if (!pooledObjectsDict.ContainsKey(listOfPooledObjects[i].prefabsToPool[j].prefab))
                {
                    List<GameObject> pooledObjects = new List<GameObject>();
                    for (int k = 0; k < listOfPooledObjects[i].prefabsToPool[j].amountToPool; k++)
                    {
                        GameObject obj = Instantiate(listOfPooledObjects[i].prefabsToPool[j].prefab);
                        obj.transform.SetParent(GameManager.Instance.poolHolders[i].transform);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                    }
                
                    pooledObjectsDict.Add(listOfPooledObjects[i].prefabsToPool[j].prefab, pooledObjects);
                }
            }
        }
    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        if (pooledObjectsDict.ContainsKey(prefab))
        {
            List<GameObject> pooledObjects = pooledObjectsDict[prefab];
            foreach (GameObject obj in pooledObjects)
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }
        return null;
    }
}
