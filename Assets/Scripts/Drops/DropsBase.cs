using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsBase : MonoBehaviour
{
    [SerializeField] private float bobSpeed;
    [SerializeField] private float amplitude;
    [SerializeField] private float vacuumSpeed;
    private float timeOffset;
    private bool vacuuming;
    private Vector3 startPos;
    private GameObject player;
    private PlayerBase playerBase;

    private void Awake()
    {
        player = GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
    }

    private void OnEnable()
    {
        startPos = transform.position;
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
    private void HandleBobbing()
    {
        if (vacuuming) return;
        float yOffset = amplitude * Mathf.Sin(bobSpeed * (Time.time - timeOffset));
        Vector3 newPos = startPos + new Vector3(0.0f, yOffset, 0.0f);
        transform.position = newPos;
    }

    private void HandleVacuum()
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
