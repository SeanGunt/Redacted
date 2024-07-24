using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Snake : EnemyMaster
{
    private bool closeToPlayer;
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject fullHealthbar;
    private SnakeAnimation snakeAnimation;
    private Animator animator;
    private Vector2 dashLocation;
    private Vector2 dashStartPosition;
    private State state;
    private enum State
    {
        moving, openingJaw, startDash, dashing, tired
    }

    protected override void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        snakeAnimation = GetComponentInChildren<SnakeAnimation>();
        state = State.moving;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;

        health = maxHealth;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }

    protected override void Update()
    {
        switch(state)
        {
            case State.moving:
                Rotation();
                Movement();
                if (DistanceToPlayer() <= 10f)
                {
                    state = State.openingJaw;
                    animator.SetTrigger("Attacking");
                }
            break;
            case State.openingJaw:
                Rotation();
                if (snakeAnimation.jawOpened)
                {
                    state = State.startDash;
                    snakeAnimation.JawClosed();
                }
            break;
            case State.startDash:
                GetPlayersPosition();
                state = State.dashing;
            break;
            case State.dashing:
                agent.enabled = false;
                Dash();
            break;
            case State.tired:

            break;
        }
    }

    protected override void Rotation()
    {
        if (player.transform.localPosition.x < transform.position.x + 0.84f)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Dash()
    {
        transform.position = Vector3.Lerp(transform.position, dashLocation, Time.deltaTime * 2f);
        Debug.Log("Dashing! at position: " + transform.position);
    }

    private void GetPlayersPosition()
    {
        dashStartPosition = transform.position;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float dashDistance = Vector2.Distance(transform.position, player.transform.position) * 3;
        dashLocation = (Vector2)transform.position + direction * dashDistance;
    }


    protected override void Movement()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, 1f);
        agent.SetDestination(target);
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }
}
