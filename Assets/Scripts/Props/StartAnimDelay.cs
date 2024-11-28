using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartAnimDelay : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnBecameVisible()
    {
        animator.enabled = true;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }

    private void OnBecameInvisible()
    {
        animator.enabled = false;
    }
}
