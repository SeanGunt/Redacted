using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsBase : MonoBehaviour
{
    [SerializeField] protected float bobSpeed;
    [SerializeField] protected float amplitude;
    [SerializeField] protected float vacuumSpeed;
    [SerializeField] protected AudioClip pickupAudioClip;
    protected float timeOffset;
    protected float vacuumSpeedChange;
    protected bool vacuuming;
    protected Vector3 startPos;
    protected Vector3 awayPosition;
    protected State state;
    protected TrailRenderer trailRenderer;
    protected SpriteRenderer spriteRenderer;
    protected GameObject player;
    protected PlayerBase playerBase;
    protected enum State
    {
        away, towards
    }


    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.startColor = GetComponent<SpriteRenderer>().color;
        trailRenderer.endColor = GetComponent<SpriteRenderer>().color;
    }

    protected virtual void OnEnable()
    {
        player = GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
        startPos = transform.position;
        vacuumSpeedChange = 0f;
        timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void OnDisable()
    {
        vacuuming = false;
    }

    private void Update()
    {
        HandleBobbing();
        HandleVacuum();
    }

    public void StartVacuuming()
    {
        vacuuming = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            
        }
    } 
    private void HandleBobbing()
    {
        if (vacuuming) return;
        float yOffset = amplitude * Mathf.Sin(bobSpeed * (Time.time - timeOffset));
        Vector3 newPos = startPos + new Vector3(0.0f, yOffset, 0.0f);
        transform.position = newPos;
    }

    protected virtual void HandleVacuum()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerBase.pickupRange && !vacuuming)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            trailRenderer.enabled = true;
            awayPosition = transform.position + direction * (distanceToPlayer * 1.2f);
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
}
