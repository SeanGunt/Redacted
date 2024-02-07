using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainLightningTrigger : ProjectileTrigger
{
    [SerializeField] private Transform lightningPivot;
    private void Awake()
    {
        StartCoroutine(ExtendBolt());
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(weaponBase.ApplyWDamage()/3);
        }
    }

    private IEnumerator ExtendBolt()
    {
        while (lightningPivot.localScale.y <= 4)
        {
            lightningPivot.transform.localScale += new Vector3(0f, 25f * Time.deltaTime, 0f);
            yield return null;
        }
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (sr.color.a >= 0)
        {
            sr.color -= new Color(0f,0f,0f, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
