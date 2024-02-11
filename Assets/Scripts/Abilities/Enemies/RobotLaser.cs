using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLaser : MonoBehaviour
{
    [SerializeField] private Destroy destroy;
    public bool fadeStarted;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color -= new Color(0f, 0f, 0f, 3* Time.deltaTime);
            yield return null;
        }
        destroy.DestroyGameObject();
    }
}
