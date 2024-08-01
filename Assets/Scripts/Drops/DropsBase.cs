using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropsBase : MonoBehaviour
{
    [SerializeField] private float bobSpeed;
    [SerializeField] private float amplitude;
    [SerializeField] protected float vacuumSpeed;
    private float timeOffset;
    protected bool vacuuming;
    private Vector3 startPos;
    protected SpriteRenderer spriteRenderer;
    protected GameObject player;
    protected PlayerBase playerBase;


    protected virtual void Awake()
    {
        player = GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void OnDisable()
    {
        vacuuming = false;
    }

    protected virtual void Start()
    {

    }
    private void Update()
    {
        HandleBobbing();
        HandleVacuum();
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
        if (distanceToPlayer <= playerBase.pickupRange)
        {
            vacuuming = true;
        }

        if (vacuuming)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * vacuumSpeed);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    } 
}
