using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDrop : DropsBase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
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
        DropsManager.instance.speedPickupActive = true;
        DropsManager.instance.speedTimer += 3.0f;
        playerBase.speed += playerBase.baseSpeed * 1f;
        while (DropsManager.instance.speedTimer > 0f)
        {
            DropsManager.instance.speedTimer -= Time.deltaTime;
            yield return null;
        }
        playerBase.speed -= playerBase.baseSpeed * 1f;
        DropsManager.instance.speedPickupActive = false;
        gameObject.SetActive(false);
    }

    private void ExtendTimer()
    {
        DropsManager.instance.speedTimer += 3.0f;
    }
}
