using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    WeaponBase weaponBase;
    private void Awake()
    {
        GameObject player = GameManager.Instance.player;
        weaponBase = player.GetComponentInChildren<WeaponBase>();

        StartCoroutine(Detonate());
    }
    private void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 7f, ~9);
        foreach (Collider2D collider2D in collider2Ds)
        {
            Transform enemyTransform = collider2D.gameObject.transform;
            Vector2 targetPosition = transform.position;
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, targetPosition, Time.deltaTime * 3f);
        }
    }

    private IEnumerator Detonate()
    {
        float detonateTimer = 5.0f;
        while (detonateTimer >= 0f)
        {
            detonateTimer -=  Time.deltaTime;
            yield return null;
        }
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 7f, ~9);
        foreach (Collider2D collider2D in collider2Ds)
        {
            IDamagable damagable = collider2D.gameObject.GetComponent<IDamagable>();
            damagable.TakeDamage(weaponBase.ApplyRDamage());
        }
        Destroy(gameObject);
        yield return null;
    }
}
