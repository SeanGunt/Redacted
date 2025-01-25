using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffOfTheStars : ItemBase
{
    [Header("Item Specific Data")]
    [SerializeField] private GameObject[] barrelDrops;
    public override void ActiveAbility()
    {
        if (activeCooldown <= 0)
        {
            int randomInt = Random.Range(0,4);
            Instantiate(barrelDrops[randomInt], player.transform.position, Quaternion.identity, GameManager.Instance.poolHolders[1].transform);
            StartCoroutine(HandleItemCooldown());
        }
    }
}
