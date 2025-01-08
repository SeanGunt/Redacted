using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VoidDrop : DropsBase
{
    [SerializeField] private GameObject timerBar;
    private Volume voidVolume;
    protected override void OnEnable()
    {
        base.OnEnable();
        voidVolume = GetComponentInChildren<Volume>();
        spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
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
        GameObject timer = Instantiate(timerBar, Vector3.zero, Quaternion.identity, GameManager.Instance.player.GetComponent<PlayerUI>().pickupTimerHolder.transform);
        Image[] timerImage = timer.GetComponentsInChildren<Image>();
        timerImage[1].color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        timerImage[1].fillAmount = 1f;
        DropsManager.instance.voidPickupActive = true;
        while (voidVolume.weight <= 1f)
        {
            voidVolume.weight += Time.deltaTime * 2f;
            yield return null;
        }
        DropsManager.instance.voidTimer = 10f;
        DropsManager.instance.voidImageTimer = DropsManager.instance.voidTimer;
        while (DropsManager.instance.voidTimer >= 0f)
        {
            DropsManager.instance.voidTimer -= Time.deltaTime;
            timerImage[1].fillAmount -= Time.deltaTime / DropsManager.instance.voidImageTimer;
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
        Destroy(timer);
        gameObject.SetActive(false);
    }

    private void ExtendTimer()
    {
        DropsManager.instance.voidImageTimer = 10f + DropsManager.instance.voidTimer;
        DropsManager.instance.voidTimer += 10f;
        Image[] timerImage = GameManager.Instance.player.GetComponent<PlayerUI>().pickupTimerHolder.GetComponentsInChildren<Image>();
        timerImage[1].fillAmount = 1f;
    }
}
