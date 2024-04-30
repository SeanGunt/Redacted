using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrigger : ProjectileTrigger
{
    public Transform[] chainPoints;
    public GameObject chainLightningPrefab;
    private List<Transform> enemyList = new List<Transform>();
    int enemyLayerMask = 1 << 9;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void HandleOtherOnHitLogic(Collider2D other)
    {
        enemyList.Clear();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(other.gameObject.transform.position, 5f, enemyLayerMask);

        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.gameObject != other.gameObject)
            {
                enemyList.Add(collider2D.gameObject.transform);
            }
        }

        Invoke("Chain", 0.2f);
    }

    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyWDamage());
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
        }
    }

    private void HandleRotation(Vector3 pos, Transform thingToRotate)
    {
        Vector3 direction = (pos - thingToRotate.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        thingToRotate.eulerAngles = new Vector3(0,0,angle);
    }
}
