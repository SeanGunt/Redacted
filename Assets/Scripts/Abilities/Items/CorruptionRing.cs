using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionRing : ProjectileTrigger
{
    private bool damagingEnemy;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagingEnemy = true;
            StartCoroutine(DoDamage(damagable));
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagingEnemy = false;
        }
    }

    protected virtual IEnumerator DoDamage(IDamagable damagable)
    {
        float damageInterval = 0f;
        while (damagingEnemy)
        {
            damageInterval -= Time.deltaTime;
            if (damageInterval <= 0)
            {
                damagable.TakeDamage(30f);
                damageInterval = 0.2f;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
