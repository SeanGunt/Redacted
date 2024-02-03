using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrigger : ProjectileTrigger
{
    public Transform[] chainPoints;
    public GameObject chainLightningPrefab;
    private List<Transform> enemyList = new List<Transform>();
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(weaponBase.ApplyWDamage());
            enemyList.Clear();
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(other.gameObject.transform.position, 5f, ~9);

            foreach (Collider2D collider2D in collider2Ds)
            {
                if (collider2D.gameObject != other.gameObject)
                {
                    enemyList.Add(collider2D.gameObject.transform);
                }
            }

            Invoke("Chain", 0.2f);
        }
    }

    private void Chain()
    {
        int maxChainedEnemies = Mathf.Min(enemyList.Count, 1);

        for (int i = 0; i < maxChainedEnemies; i++)
        {
            if (enemyList[i] == null) return;
            int randomChainPoint = Random.Range(0,2);
            GameObject chainLightning = Instantiate(chainLightningPrefab, chainPoints[randomChainPoint].position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
            HandleRotation(enemyList[i].transform.position, chainLightning.transform);
            StartCoroutine(ExtendBolt(chainLightning.transform));
        }
    }

    private IEnumerator ExtendBolt(Transform lightning)
    {
        while (lightning.localScale.y <= 4)
        {
            lightning.transform.localScale += new Vector3(0f, 25f * Time.deltaTime, 0f);
            yield return null;
        }
        BoxCollider2D bc = lightning.GetComponentInChildren<BoxCollider2D>();
        bc.enabled = false;
        SpriteRenderer sr = lightning.GetComponentInChildren<SpriteRenderer>();
        while (sr.color.a >= 0)
        {
            sr.color -= new Color(0f,0f,0f, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Destroy(lightning.gameObject);
        yield return null;
    }

    private void HandleRotation(Vector3 pos, Transform thingToRotate)
    {
        Vector3 direction = (pos - thingToRotate.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        thingToRotate.eulerAngles = new Vector3(0,0,angle);
    }
}
