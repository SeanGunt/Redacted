using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrigger : ProjectileTrigger
{
    [SerializeField] private float damageMultiplier;
    [SerializeField] private GameObject smallerFireball;
    private HashSet<int> hitEnemies = new HashSet<int>();
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            EnemyMaster enemy = other.gameObject.GetComponent<EnemyMaster>();
            if (enemy != null && !hitEnemies.Contains(enemy.enemyID))
            {
                hitEnemies.Add(enemy.enemyID);
                player = GameManager.Instance.player;
                weaponBase = player.GetComponentInChildren<WeaponBase>();
                HandleDamageSelection(damageToApply);
                damagable.TakeDamage(damageToApply);
                HandleOtherOnHitLogic(other);
            }
        }
    }

    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyQDamage() * damageMultiplier);
    }

    protected override void HandleOtherOnHitLogic(Collider2D other)
    {
        if (smallerFireball == null)
        {
            GetComponent<ProjectileInvisible>().StopParticles();
            Destroy(gameObject);
            return;
        }

        GameObject newFireball = Instantiate(smallerFireball, transform.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        FireballTrigger newFireballTrigger = newFireball.GetComponent<FireballTrigger>();
        if (newFireballTrigger != null)
        {
            newFireballTrigger.hitEnemies = new HashSet<int>(hitEnemies);
        }

        GetComponent<ProjectileInvisible>().StopParticles();
        Destroy(gameObject);
    }
}
