using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VoidDrop : DropsBase
{
    private Volume voidVolume;
    protected override void OnEnable()
    {
        base.OnEnable();
        voidVolume = GetComponentInChildren<Volume>();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.0f); //This must happen otherwise the pickup will remain rendered
            trailRenderer.enabled = false;
            if (!DropsManager.instance.voidPickupActive)
            {
                StartCoroutine(HandleVoidEffects());
            }
            else
            {
                ExtendTimer();
            }
        }
    }

    private IEnumerator HandleVoidEffects()
    {
        DropsManager.instance.voidPickupActive = true;
        while (voidVolume.weight <= 1f)
        {
            voidVolume.weight += Time.deltaTime * 2f;
            yield return null;
        }
        DropsManager.instance.voidTimer = 10f;
        while (DropsManager.instance.voidTimer >= 0f)
        {
            DropsManager.instance.voidTimer -= Time.deltaTime;
            GameManager.Instance.FreezeTime();
            yield return null;
        }
        GameManager.Instance.UnFreezeTime();

        while (voidVolume.weight >= 0f)
        {
            voidVolume.weight -= Time.deltaTime;
            yield return null;
        }
        DropsManager.instance.voidPickupActive = false;
        gameObject.SetActive(false);
    }

    private void ExtendTimer()
    {
        DropsManager.instance.voidTimer += 10f;
    }
}
