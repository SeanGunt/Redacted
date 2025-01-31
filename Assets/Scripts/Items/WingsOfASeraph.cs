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
            Instantiate(playerOSU, playerBase.GetMousePosition(), Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
            StartCoroutine(HandleItemCooldown());
        }
    }
}
