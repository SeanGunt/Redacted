using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCircleTrigger : ProjectileTrigger
{
    [SerializeField] private float radius;
    [SerializeField] private float growthSpeed;
    private PolygonCollider2D polygonCollider2D;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        StartCoroutine(Expand());
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    private IEnumerator Expand()
    {
        while (transform.localScale.x <= radius - 0.5f)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(radius, radius), Time.deltaTime * growthSpeed);

            yield return null;
        }
        polygonCollider2D.enabled = false;
        while (spriteRenderer.color.a >= 0)
        {
            spriteRenderer.color -= new Color(0,0,0, Time.deltaTime * 2);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
