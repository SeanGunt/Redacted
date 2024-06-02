using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : ProjectileTrigger
{
    protected override void HandleOtherOnHitLogic(Collider2D other)
    {
        transform.parent = other.transform;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        bc.enabled = false;
        StartCoroutine(StartFade());
    }

    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyQDamage());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            StartCoroutine(StartFade());
        }
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(1f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        while(spriteRenderer.color.a >= 0)
        {
            spriteRenderer.color -= new Color(0f,0f,0f,Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
