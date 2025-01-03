using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkReticle : MonoBehaviour
{
    private Vector3 initialSize;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        initialSize = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.color = SaveManager.instance._settingsData.reticleColor;
        transform.localScale = initialSize;
        StartCoroutine(HandleShrink());
    }

    private IEnumerator HandleShrink()
    {
        Vector3 startSize = transform.localScale;
        while (transform.localScale.x >= 0)
        {
            startSize.x -= Time.deltaTime;
            startSize.y -= Time.deltaTime;
            transform.localScale = new Vector3(startSize.x, startSize.y, 0f);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
