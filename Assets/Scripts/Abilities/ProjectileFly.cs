using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFly : MonoBehaviour
{
    private Rigidbody2D rb;
    public ParticleSystem particles;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        if (!gameObject.activeInHierarchy) return;
        particles.transform.parent = GameManager.Instance.poolHolders[4].transform;
        particles.Stop();
        Destroy(gameObject);
    }
    
}
