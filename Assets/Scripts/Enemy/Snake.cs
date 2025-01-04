using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Snake : EnemyMaster
{
    private SnakeAnimation snakeAnimation;
    private Vector2 dashLocation;
    private bool dashing;
    private float dashCooldown = 15f;
    [SerializeField] private float dashDamage;
    [Range(0f, 15f)] private float timeTillDash = 0f;
    private State state;
    private enum State
    {
        moving, openingJaw, startDash, dashing, tired
    }

    protected override void Awake()
    {
        snakeAnimation = GetComponentInChildren<SnakeAnimation>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        baseSpeed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        health = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
        state = State.moving;
    }

    protected override void Update()
    {
        HandleFrozen();
        switch(state)
        {
            case State.moving:
                Rotation();
                Movement();
                HandleDashCooldown();
                if (DistanceToPlayer() <= 6f && timeTillDash <= 0)
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
                HandleDashParams(10f, SpeedChange.increase, true, ObstacleAvoidanceType.NoObstacleAvoidance, 250f);
                agent.SetDestination(dashLocation);
                state = State.dashing;
            break;
            case State.dashing:
                if (Vector2.Distance(transform.position, dashLocation) <= 0.5f)
                {
                    animator.SetTrigger("FinishedAttacking");
                    state = State.tired;
                }
            break;
            case State.tired:
                HandleDashParams(10f, SpeedChange.decrease, false, ObstacleAvoidanceType.HighQualityObstacleAvoidance, 8f);
                timeTillDash = dashCooldown;
                state = State.moving;
            break;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player && dashing)
        {
            playerBase.TakeDamage(dashDamage, Mathf.Log(playerBase.physicalResistance, 10000));
        }

        if (other.gameObject == player && !dashing)
        {
            damagingPlayer = true;
            StartCoroutine(DoDamage());
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

    private void GetPlayersPosition()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float dashDistance = Vector2.Distance(transform.position, player.transform.position) * 1.5f;
        dashLocation = (Vector2)transform.position + direction * dashDistance;
    }

    private void HandleDashParams(float speedToChange, SpeedChange speedChange, bool isDashing, ObstacleAvoidanceType obstacleAvoidanceType, float acceleration)
    {
        ChangeSpeed(speedToChange, speedChange);
        dashing = isDashing;
        agent.obstacleAvoidanceType = obstacleAvoidanceType;
        agent.acceleration = acceleration;
    }

    private void HandleDashCooldown()
    {
        timeTillDash -= Time.deltaTime;
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
