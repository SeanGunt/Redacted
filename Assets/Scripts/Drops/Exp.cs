using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Exp : DropsBase
{
    [SerializeField] private float expAmount;
    private Vector3 awayPosition;
    private ExperienceManager experienceManager;
    private TrailRenderer trailRenderer;
    private State state;
    private float vacuumSpeedChange;
    private enum State
    {
        away, towards
    }

    protected override void Awake()
    {
        base.Awake();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.startColor = GetComponent<SpriteRenderer>().color;
        trailRenderer.endColor = GetComponent<SpriteRenderer>().color;
    }

    protected override void Start()
    {
        experienceManager = player.GetComponent<ExperienceManager>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        vacuumSpeedChange = 0f;
        trailRenderer.enabled = false;
    }

    protected override void HandleVacuum()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerBase.pickupRange && !vacuuming)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            awayPosition = transform.position + direction * (distanceToPlayer * 1.2f);
            trailRenderer.enabled = true;
            state = State.away;
            vacuuming = true;
        }

        if (vacuuming)
        {
            switch (state)
            {
                case State.away:
                    transform.position = Vector3.Lerp(transform.position, awayPosition, Time.deltaTime * vacuumSpeed);
                    float distance = Vector3.Distance(transform.position, awayPosition);
                    if (distance <= 0.6f)
                    {
                        state = State.towards;
                    }
                break;
                case State.towards:
                    vacuumSpeedChange += Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * (vacuumSpeed + vacuumSpeedChange));
                break;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            experienceManager.IncreaseExperience(expAmount);
            gameObject.SetActive(false);
        }
    }
}
