using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject[] prefabsToPool;
    private Dictionary<GameObject, List<GameObject>> pooledObjectsDict = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        instance = this;
    }

    public void InitiaizePool(GameObject prefab, int amountToPool, GameObject[] poolHolder, int index)
    {
        if (!pooledObjectsDict.ContainsKey(prefab))
        {
            List<GameObject> pooledObjects = new List<GameObject>();
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.transform.SetParent(poolHolder[index].transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
            pooledObjectsDict.Add(prefab, pooledObjects);
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
