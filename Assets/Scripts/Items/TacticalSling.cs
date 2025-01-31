using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalSling : ItemBase
{
    private void Start()
    {
        PassiveAbility();
    }
    public override void ActiveAbility()
    {
        base.ActiveAbility();
    }

    public override void PassiveAbility()
    {
       weaponBase.animator.speed += 0.25f;
    }
}
