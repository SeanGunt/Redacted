using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTestController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float swordDamage;
    [HideInInspector] public bool inAnimation;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(swordDamage);
        }
    }
    public void HandleSwordSwingAnim(string animToPlay)
    {
        animator.SetTrigger(animToPlay);
    }

    public void StartAnim()
    {
        inAnimation = true;
    }

    public void EndAnim()
    {
        inAnimation = false;
    }
}
