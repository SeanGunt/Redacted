using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestIsometricPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 positionToMove;
    private float speed = 3f;
    private State state;
    private enum State
    {
        idle, moving
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.idle;
    }

    private void Update()
    {   switch(state)
        {
            case State.idle:
                HandleIdle();
            break;
            case State.moving:
                HandleMoving();
            break;
        }
        RightClick();
    }
    private void RightClick()
    {
        if (playerInput.actions["RightClick"].triggered && Time.timeScale > 0f)
        {
            positionToMove = GetMousePosition();
            if (positionToMove.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (positionToMove.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            state = State.moving;
        }
    }

    public Vector3 GetMousePosition()
    {
       Vector3 mousePos = playerInput.actions["PointerPosition"].ReadValue<Vector2>();
       mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       mousePos.z = 0;
       return mousePos;
    }

    private void HandleMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionToMove, speed * Time.deltaTime);
        animator.SetBool("isWalking", true);
        float distanceToWalkPosition = Vector2.Distance(transform.position, positionToMove);
        if (distanceToWalkPosition < 0.01f)
        {
            state = State.idle;
        }
    }

    private void HandleIdle()
    {
        animator.SetBool("isWalking", false);
    }
}
