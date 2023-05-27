using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamagable
{
    void TakeDamage(float damage);
}
public class EnemyMaster : MonoBehaviour, IDamagable
{
    [SerializeField] private float health, speed, damage, damageInterval;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
    protected virtual void Movement()
    {
        Debug.Log("Hello");
    }
}
