using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replicator : ItemBase
{
    private void Start()
    {
        PassiveAbility();
    }
    public override void PassiveAbility()
    {
        weaponBase.replicatorPurchased = true;
    }
}
