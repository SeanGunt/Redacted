using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainLightningTrigger : ProjectileTrigger
{
    [SerializeField] private Transform lightningPivot;
    [SerializeField] private Destroy destroy;
    private void Awake()
    {
        StartCoroutine(ExtendAndFadeBolt());
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyWDamage()/3);
    }

    private IEnumerator ExtendAndFadeBolt()
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
        destroy.DestroyGameObject();
    }
}
