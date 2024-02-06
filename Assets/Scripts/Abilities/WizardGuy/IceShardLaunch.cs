using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float destroyTimer = 5.0f;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        rb.velocity = transform.up * speed;
        boxCollider2D.enabled = true;
    }
}
