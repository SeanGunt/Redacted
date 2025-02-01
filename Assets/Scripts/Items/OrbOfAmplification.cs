using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfAmplification : ItemBase
{
    [HideInInspector] public float amountToAmplify;
    private void Start()
    {
        PassiveAbility();
    }
    public override void PassiveAbility()
    {
        playerBase.magicalDamageMultiplier += 0.2f;
    }
}
