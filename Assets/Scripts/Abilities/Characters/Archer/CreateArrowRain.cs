using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArrowRain : MonoBehaviour
{
    public GameObject arrowRPrefab;
    private Bow bow;
    private float YspawnPos;

    private void Awake()
    {
        bow = GameManager.Instance.player.GetComponentInChildren<Bow>();
    }
    private void OnBecameInvisible()
    {
        YspawnPos = transform.position.y;
        StartCoroutine(HandleArrowRain());
    }

    private IEnumerator HandleArrowRain()
    {
        int numOfArrows = 20;
        int arrowsSpawned = 0;
        while (numOfArrows >= arrowsSpawned)
        {
            float randomXOffset = Random.Range(-2.5f, 2.5f);
            GameObject arrow = Instantiate(arrowRPrefab, new Vector2(bow.mousePos.x + randomXOffset, YspawnPos + 5f), Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
            arrow.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            arrowsSpawned += 1;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
