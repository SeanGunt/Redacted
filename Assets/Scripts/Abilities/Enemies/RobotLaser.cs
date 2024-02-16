using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLaser : MonoBehaviour
{
    [SerializeField] private Destroy destroy;
    private BoxCollider2D boxCollider2D;
    public bool fadeStarted;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {   
        if (fadeStarted)
        {
            StartCoroutine(Fade());
            fadeStarted = false;
        }
    }
    public IEnumerator Fade()
    {
        boxCollider2D.enabled = false;
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color -= new Color(0f, 0f, 0f, 3* Time.deltaTime);
            yield return null;
        }
        destroy.DestroyGameObject();
    }
}
