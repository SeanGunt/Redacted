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
    [SerializeField] private float maxHealth, speed, damage;
    private float health;
    [SerializeField] private RectTransform healhBar;
    private GameObject player;
    private PlayerBase playerBase;
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
        playerBase = player.GetComponent<PlayerBase>();
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerBase.health -= damage;
            float ratio = damage / playerBase.baseHealth;
            playerBase.healthBar.fillAmount -= Mathf.Clamp(ratio, 0f, 1f);
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
