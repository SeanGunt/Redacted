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
    [SerializeField] private float maxHealth, speed, damage, damageInterval;
    private float health;
    [SerializeField] private RectTransform healhBar;
    private GameObject player;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;

        health = maxHealth;
    }

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        Movement();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        float ratio = maxHealth / 100;
        float damageToBar = damage / ratio;
        healhBar.sizeDelta -= new Vector2(damageToBar, 0);

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
        agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
    }
}
