using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceballLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float constantSpeed;
    [SerializeField] private float decelerationRate;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * initialSpeed;
    }

    private void Update()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity.normalized * constantSpeed, decelerationRate * Time.deltaTime);
    }
}
