using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected PlayerBase playerBase;
    [HideInInspector] public bool canRotate = true;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public AbilityType abilityType;
    public float qDamage;
    public float wDamage;
    public float eDamage;
    public float rDamage;
    [SerializeField] private float qPhysRatio, wPhysRatio, ePhysRatio, rPhysRatio;
    [SerializeField] private float qMagRatio, wMagRatio, eMagRatio, rMagRatio;
    protected float damageToApply;
    public enum AbilityType
    {
        Q, W, E, R
    }

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
    }

    protected virtual float ApplyDamage()
    {
        switch(abilityType)
        {
            case AbilityType.Q:
                damageToApply = qDamage + DamageScaling(qPhysRatio, qMagRatio);
                Debug.Log("Q Damage: " + damageToApply);
                break;
            case AbilityType.W:
                damageToApply = wDamage + DamageScaling(wPhysRatio, wMagRatio);
                Debug.Log("W Damage: " + damageToApply);
                break;
            case AbilityType.E:
                damageToApply = eDamage + DamageScaling(ePhysRatio, eMagRatio);
                Debug.Log("E Damage: " + damageToApply);
                break;
            case AbilityType.R:
                damageToApply = rDamage + DamageScaling(rPhysRatio, rMagRatio);
                Debug.Log("R Damage: " + damageToApply);
                break;
        }
        return damageToApply;
    }

    protected float DamageScaling(float physRat, float magRat)
    {
        float damageScaling = (playerBase.physicalDamage * physRat) + (playerBase.magicalDamage * magRat);
        return damageScaling;
    }
}
