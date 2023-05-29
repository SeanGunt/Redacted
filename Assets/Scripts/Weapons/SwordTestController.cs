using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTestController : MonoBehaviour
{
    private Animator animator;
    private TrailRenderer trailRenderer;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [SerializeField] private float weaponDamage;
    [HideInInspector] public bool inAnimation; 
    [HideInInspector] public bool canRotate {get; private set;} = true;
    [HideInInspector] public bool canMove {get; private set;} = true;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        weaponCollider = this.GetComponent<BoxCollider2D>();
        trailRenderer = this.GetComponentInChildren<TrailRenderer>();
        weaponCollider.enabled = false;
        trailRenderer.emitting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.gameObject.name);
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(weaponDamage);
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
