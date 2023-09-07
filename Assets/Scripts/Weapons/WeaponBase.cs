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
    public int qLevel = 0;
    public int wLevel = 0;
    public int eLevel = 0;
    public int rLevel = 0;
    [SerializeField] private float qBaseScaling;
    [SerializeField] private float wBaseScaling;
    [SerializeField] private float eBaseScaling;
    [SerializeField] private float rBaseScaling;
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
                damageToApply = BaseDamage(qDamage, qBaseScaling, qLevel) + DamageScaling(qPhysRatio, qMagRatio);
                Debug.Log("Q Damage: " + damageToApply);
                break;
            case AbilityType.W:
                damageToApply = BaseDamage(wDamage, wBaseScaling, wLevel) + DamageScaling(wPhysRatio, wMagRatio);
                Debug.Log("W Damage: " + damageToApply);
                break;
            case AbilityType.E:
                damageToApply = BaseDamage(eDamage, eBaseScaling, eLevel) + DamageScaling(ePhysRatio, eMagRatio);
                Debug.Log("E Damage: " + damageToApply);
                break;
            case AbilityType.R:
                damageToApply = BaseDamage(rDamage, rBaseScaling, rLevel) + DamageScaling(rPhysRatio, rMagRatio);
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

    protected float BaseDamage(float abilityDamage, float abilityDamageScaling, float abilityLevel)
    {
        float baseDamage = abilityDamage + (abilityLevel * abilityDamageScaling);
        return baseDamage;
    }
}
