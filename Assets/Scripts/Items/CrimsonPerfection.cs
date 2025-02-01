using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonPerfection : ItemBase
{
    private void Start()
    {
        PassiveAbility();
    }
    public override void PassiveAbility()
    {
        weaponBase.crimsonPerfectionPurchased = true;
    }
}
