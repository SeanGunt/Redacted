using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpeedDrop : DropsBase
{
    [SerializeField] private GameObject timerBar;
    [SerializeField] private GameObject shadow;
    protected override void OnEnable()
    {
        base.OnEnable();
        shadow.SetActive(true);
        spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player && bc.enabled)
        {
            bc.enabled = false;
            shadow.SetActive(false);
            SFXManager.instance.PlayOneShotAtPoint(transform.position, pickupAudioClip);
            spriteRenderer.color = new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.0f); //This must happen otherwise the pickup will remain rendered
            trailRenderer.enabled = false;
            if (!DropsManager.instance.speedPickupActive)
            {
                StartCoroutine(IncreaseSpeed());
            }
            else
            {
                ExtendTimer();
            }
        }
    
    }

    private IEnumerator IncreaseSpeed()
    {
        trailRenderer.enabled = false;
        GameObject timer = Instantiate(timerBar, Vector3.zero, Quaternion.identity, GameManager.Instance.player.GetComponent<PlayerUI>().pickupTimerHolder.transform);
        Image[] timerImage = timer.GetComponentsInChildren<Image>();
        timerImage[1].color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

        DropsManager.instance.speedPickupActive = true;
        DropsManager.instance.speedTimer += 3.0f;
        DropsManager.instance.speedImageTimer = DropsManager.instance.speedTimer;
        playerBase.speed += playerBase.baseSpeed * 1f;
        while (DropsManager.instance.speedTimer > 0f)
        {
            DropsManager.instance.speedTimer -= Time.deltaTime;
            timerImage[1].fillAmount -= Time.deltaTime / DropsManager.instance.speedImageTimer;

            yield return null;
        }

        playerBase.speed -= playerBase.baseSpeed * 1f;
        DropsManager.instance.speedPickupActive = false;

        Destroy(timer);
        gameObject.SetActive(false);
    }

    private void ExtendTimer()
    {
        DropsManager.instance.speedImageTimer = 3.0f + DropsManager.instance.speedTimer;
        DropsManager.instance.speedTimer += 3.0f;
        Image[] timerImage = GameManager.Instance.player.GetComponent<PlayerUI>().pickupTimerHolder.GetComponentsInChildren<Image>();
        timerImage[1].fillAmount = 1f;
    }

}
