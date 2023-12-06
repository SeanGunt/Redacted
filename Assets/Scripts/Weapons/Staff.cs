using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : WeaponBase
{
    [SerializeField] public ParticleSystem ps;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [HideInInspector] public bool inAnimation;

    // Start is called before the first frame update
    void Start()
    {   
        weaponCollider = GetComponent<BoxCollider2D>();
        weaponCollider.enabled = false;
    }

    #region Animation Events
    public void HandleStaffThrustAnim(string animToPlay)
    {
        animator.SetTrigger(animToPlay);
    }

    public void HandleStaffSwingAnim(string animToPlay)
    {
        animator.SetTrigger(animToPlay);
    }
    
    #endregion
}
