using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
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
                damageToApply = CalculateDamage(qDamage, qBaseScaling, qLevel, qPhysRatio, qMagRatio);
                break;
            case AbilityType.W:
                damageToApply = CalculateDamage(wDamage, wBaseScaling, wLevel, wPhysRatio, wMagRatio);
                break;
            case AbilityType.E:
                damageToApply = CalculateDamage(eDamage, eBaseScaling, eLevel, ePhysRatio, eMagRatio);
                break;
            case AbilityType.R:
                damageToApply = CalculateDamage(rDamage, rBaseScaling, rLevel, rPhysRatio, rMagRatio);
                break;
        }
        return damageToApply;
    }

    public float ApplyQDamage()
    {
        return CalculateDamage(qDamage, qBaseScaling, qLevel, qPhysRatio, qMagRatio);
    }

    public float ApplyWDamage()
    {
        return CalculateDamage(wDamage, wBaseScaling, wLevel, wPhysRatio, wMagRatio);
    }

    public float ApplyEDamage()
    {
        return CalculateDamage(eDamage, eBaseScaling, eLevel, ePhysRatio, eMagRatio);
    }

    public float ApplyRDamage()
    {
        return CalculateDamage(rDamage, rBaseScaling, rLevel, rPhysRatio, rMagRatio);
    }

    protected float CalculateDamage(float abilityDamage, float abilityDamageScaling, float abilityLevel, float physRat, float magRat)
    {
        float finalDamage = BaseDamage(abilityDamage, abilityDamageScaling, abilityLevel) + DamageScaling(physRat, magRat);
        playerBase.attacksUsed += 1;
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
