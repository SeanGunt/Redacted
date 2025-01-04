using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezable : MonoBehaviour, IFreezable
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void HandleOnFreeze()
    {
        animator.speed = 0f;
    }

    public void HandleOnUnfreeze()
    {
        animator.speed = 1f;
    }
}
