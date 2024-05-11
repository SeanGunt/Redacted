using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private TrailRenderer trailRenderer;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [HideInInspector] public bool inAnimation;
    private void Start()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        weaponCollider.enabled = false;
        trailRenderer.emitting = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(ApplyDamage());
        }
    }

    public void HandleSwordAnims(string abilityName)
    {
        animator.SetTrigger(abilityName);
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        trailRenderer.emitting = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        trailRenderer.emitting = false;
    }
}
