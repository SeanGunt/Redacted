using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulwark : ItemBase
{
    private void Start()
    {
       PassiveAbility();
    }
    public override void PassiveAbility()
    {
        playerBase.damageReduction += 0.1f;
    }
}
