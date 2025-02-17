using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsOfASeraph : ItemBase
{
    [Header("Item Specific Data")]
    [SerializeField] private GameObject playerOSU;
    public override void ActiveAbility()
    {
        if (activeCooldown <= 0)
        {
            StartCoroutine(PillarsOfSalt());
            StartCoroutine(HandleItemCooldown());
        }
    }

    private IEnumerator PillarsOfSalt()
    {
        float activeTimer = 7f;
        float pillarTimer = 0f;
        while (activeTimer >= 0)
        {
            activeTimer -= Time.deltaTime;
            pillarTimer -= Time.deltaTime;
            while (pillarTimer <= 0)
            {
                Vector2 randomPosition = new Vector2(player.transform.position.x + Random.Range(-3f, 3f), player.transform.position.y + Random.Range(-3f, 3f));
                Instantiate(playerOSU, randomPosition, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
                pillarTimer = 0.5f;
                yield return null;
            }
            yield return null;
        }
    }
}
