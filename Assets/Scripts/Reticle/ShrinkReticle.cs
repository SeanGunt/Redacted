using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkReticle : MonoBehaviour
{
    private Vector3 initialSize;
    private void Awake()
    {
        initialSize = this.transform.localScale;
    }
    private void OnEnable()
    {
        this.transform.localScale = initialSize;
        StartCoroutine(HandleShrink());
    }

    private IEnumerator HandleShrink()
    {
        Vector3 startSize = this.transform.localScale;
        while (this.transform.localScale.x >= 0)
        {
            startSize.x -= Time.deltaTime;
            startSize.y -= Time.deltaTime;
            this.transform.localScale = new Vector3(startSize.x, startSize.y, 0f);
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}
