using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponBase : MonoBehaviour
{
    protected PlayerBase playerBase;
    protected Animator animator;
    [HideInInspector] public AnimationClip[] clips;
    [HideInInspector] public Material defaultMaterial;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public AbilityType abilityType;
    [HideInInspector] public float damageToApply;
    [HideInInspector] public float damageMultiplier = 1.0f;
    [HideInInspector] public bool canRotate = true;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool wasCriticalHit;
    [HideInInspector] public float qMultiplier = 1.0f;
    [HideInInspector] public float wMultiplier = 1.0f;
    [HideInInspector] public float eMultiplier = 1.0f;
    [HideInInspector] public float rMultiplier = 1.0f;
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
    public enum AbilityType
    {
        Q, W, E, R
    }

    public enum AbilityMultiplierType
    {
        Q, W, E, R
    }

    protected virtual void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clips = animator.runtimeAnimatorController.animationClips;
        defaultMaterial = spriteRenderer.material;
    }

    public float ApplyDamage()
    {
        switch(abilityType)
        {
            case AbilityType.Q:
                damageToApply = CalculateDamage(qDamage, qBaseScaling, qLevel, qPhysRatio, qMagRatio, qMultiplier);
                break;
            case AbilityType.W:
                damageToApply = CalculateDamage(wDamage, wBaseScaling, wLevel, wPhysRatio, wMagRatio, wMultiplier);
                break;
            case AbilityType.E:
                damageToApply = CalculateDamage(eDamage, eBaseScaling, eLevel, ePhysRatio, eMagRatio, eMultiplier);
                break;
            case AbilityType.R:
                damageToApply = CalculateDamage(rDamage, rBaseScaling, rLevel, rPhysRatio, rMagRatio, rMultiplier);
                break;
        }
        return damageToApply;
    }

    public float ApplyQDamage()
    {
        return CalculateDamage(qDamage, qBaseScaling, qLevel, qPhysRatio, qMagRatio, qMultiplier);
    }

    public float ApplyWDamage()
    {
        return CalculateDamage(wDamage, wBaseScaling, wLevel, wPhysRatio, wMagRatio, wMultiplier);
    }

    public float ApplyEDamage()
    {
        return CalculateDamage(eDamage, eBaseScaling, eLevel, ePhysRatio, eMagRatio, eMultiplier);
    }

    public float ApplyRDamage()
    {
        return CalculateDamage(rDamage, rBaseScaling, rLevel, rPhysRatio, rMagRatio, rMultiplier);
    }

    protected float CalculateDamage(float abilityDamage, float abilityDamageScaling, float abilityLevel, float physRat, float magRat, float abilityMultiplier)
    {
        float finalDamage = (BaseDamage(abilityDamage, abilityDamageScaling, abilityLevel) + DamageScaling(physRat, magRat)) * abilityMultiplier;
        playerBase.attacksUsed += 1;

        if (CriticalHit())
        {
            float critMultiplier = 2.0f;
            finalDamage *= critMultiplier;
        }
        return finalDamage * damageMultiplier;
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

    public bool CriticalHit()
    {
        if (playerBase.critChance > 0)
        {
            int randomValue = Random.Range(0, 101);
            if (randomValue < playerBase.critChance)
            {
                wasCriticalHit = true;
                return true;
            }
        }
        wasCriticalHit = false;
        return false;
    }

    public void ChangeQMultiplier(float multiplier)
    {
        qMultiplier = multiplier;
    }

    public void ChangeWMultiplier(float multiplier)
    {
        wMultiplier = multiplier;
    }

    public void ChangeEMultiplier(float multiplier)
    {
        eMultiplier = multiplier;
    }

    public void ChangeRMultiplier(float multiplier)
    {
        rMultiplier = multiplier;
    }

    public void QAbilityType()
    {
        abilityType = AbilityType.Q;
    }

    public void WAbilityType()
    {
        abilityType = AbilityType.W;
    }

    public void EAbilityType()
    {
        abilityType = AbilityType.E;
    }

    public void RAbilityType()
    {
        abilityType = AbilityType.R;
    }

    public void CanFlipSprite()
    {
        playerBase.canFlipSprite = true;
    }

    public void CantFlipSprite()
    {
        playerBase.canFlipSprite = false;
    }

    public void CanMove()
    {
        canMove = true;
    }

    public void CantMove()
    {
        canMove = false;
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void CantRotate()
    {
        canRotate = false;
    }
}
