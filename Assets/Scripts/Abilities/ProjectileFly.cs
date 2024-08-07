using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFly : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
    
}
