using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestIsoWeapon : MonoBehaviour
{
    [SerializeField] private GameObject swordGameObject;
    private Animator animator;
    private PlayerInput playerInput;
    private TestIsometricPlayer testIsometricPlayer;

    private void Start()
    {
        testIsometricPlayer = GetComponentInParent<TestIsometricPlayer>();
        playerInput = testIsometricPlayer.GetComponent<PlayerInput>();
        animator = swordGameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        HandleWeaponRotation();
        HandleQAbility();
    }

    private void HandleWeaponRotation()
    {
        Vector3 direction = (testIsometricPlayer.GetMousePosition() - transform.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }

    private void HandleQAbility()
    {
        if (playerInput.actions["QAttack"].triggered)
        {
            animator.SetTrigger("Swipe");
        }
    }
}
