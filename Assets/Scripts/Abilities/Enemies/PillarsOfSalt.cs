using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsOfSalt : MonoBehaviour
{
    [SerializeField] private Transform growthTransform;
    [SerializeField] private GameObject pillarPrefab;

    private void Awake()
    {
        StartCoroutine(Growth());
    }
    private IEnumerator Growth()
    {
        while (growthTransform.localScale.x < 1.5f)
        {
            growthTransform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0f);
            yield return null;
        }
        Instantiate(pillarPrefab, transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
        Destroy(gameObject);
    }
}
