using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private Animator animator;
    private TrailRenderer trailRenderer;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [HideInInspector] public bool inAnimation; 
    private void Start()
    {
        animator = GetComponent<Animator>();
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

    #region Animation Events
    public void HandleSwordSwingAnim(string animToPlay)
    {
        animator.SetTrigger(animToPlay);
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

    public void StartAnim()
    {
        inAnimation = true;
    }

    public void EndAnim()
    {
        inAnimation = false;
    }
    #endregion
}
