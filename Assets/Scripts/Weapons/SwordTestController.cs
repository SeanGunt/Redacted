using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTestController : MonoBehaviour
{
    private Animator animator;
    [HideInInspector] public bool inAnimation;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
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
