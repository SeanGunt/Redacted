using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [HideInInspector] public bool canRotate = true;
    [HideInInspector] public bool canMove = true;
    public float qDamage;
    public float wDamage;
    public float eDamage;
    public float rDamage;
    protected float damageToApply;
    public AbilityType abilityType;
    public enum AbilityType
    {
        Q, W, E, R
    }

    protected virtual float ApplyDamage()
    {
        switch(abilityType)
        {
            case AbilityType.Q:
                damageToApply = qDamage;
                break;
            case AbilityType.W:
                damageToApply = wDamage;
                break;
            case AbilityType.E:
                damageToApply = eDamage;
                break;
            case AbilityType.R:
                damageToApply = rDamage;
                break;
        }
        return damageToApply;
    }
}
