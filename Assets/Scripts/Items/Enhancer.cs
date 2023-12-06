using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhancer : ItemBase
{
    [Header("Item Specific Data")]
    [SerializeField] private Material enhancerMaterial;
    private int numOfDoubleDamageAttacks = 3;
    private int currNumOfAttacks;
    private bool enhancerActivated;

    public override void ActiveAbility()
    {
        if (activeCooldown <= 0)
        {
            currNumOfAttacks = playerBase.attacksUsed;
            enhancerActivated = true;
            weaponBase.damageMultiplier = weaponBase.damageMultiplier * 2.0f;
            weaponBase.spriteRenderer.material = enhancerMaterial;
            StartCoroutine(HandleItemCooldown());
        }
        
    }

    private void DeactivateAbility()
    {
        enhancerActivated = false;
        weaponBase.damageMultiplier = weaponBase.damageMultiplier / 2.0f;
        weaponBase.spriteRenderer.material = weaponBase.defaultMaterial;
    }

    public override void PassiveAbility()
    {
        
    }

    private void Update()
    {
        if (playerBase.attacksUsed >= currNumOfAttacks + numOfDoubleDamageAttacks && enhancerActivated)
        {
            DeactivateAbility();
        }

    }
}
