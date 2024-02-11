using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        transform.localScale = new Vector2(0f,0f);
        StartCoroutine(Expand());
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

    private IEnumerator Expand()
    {
        while (transform.localScale.x < 1f)
        {
            transform.localScale += new Vector3(2 * Time.deltaTime, 2 * Time.deltaTime, 0f);
            yield return null;
        }
        Launch();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
