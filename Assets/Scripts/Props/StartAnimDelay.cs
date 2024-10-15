using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimDelay : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
