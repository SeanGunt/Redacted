using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting;

public class EnemyMaster : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth, speed, damage;
    private float health;
    [SerializeField] private int moneyGainedOnKill;
    [SerializeField] private RectTransform healthBar;
    protected GameObject player;
    private PlayerBase playerBase;
    protected NavMeshAgent agent;
    protected Vector3 target;
    private SpriteRenderer spriteRenderer;
    private Material material;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;

        health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }

    private void Start()
    {
        player = GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
    }

    protected virtual void Update()
    {
        Movement();
        Rotation();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        float ratio = maxHealth / 100;
        float damageToBar = damage / ratio;
        healthBar.sizeDelta -= new Vector2(damageToBar, 0);
        SpawnDamageNumber(damage);

        if (health <= 0)
        {
            Die();
        }
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(0.1f);
        material.SetColor("_Color", Color.black);
        yield break;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerBase.TakeDamage(damage, Mathf.Log(playerBase.physicalResistance, 10000));
        }
    }

    protected virtual void Die()
    {
        GameObject exp = ObjectPool.instance.GetPooledObject(ObjectPool.instance.prefabsToPool[1]);
        if (exp != null)
        {
            exp.transform.position = transform.position;
            exp.SetActive(true);
        }
        MoneyManager.instance.AddMoney(moneyGainedOnKill);
        Destroy(gameObject);
    }

    protected virtual void SpawnDamageNumber(float damage)
    {
        GameObject num = ObjectPool.instance.GetPooledObject(ObjectPool.instance.prefabsToPool[2]);
        TextMeshProUGUI numText =  num.GetComponent<TextMeshProUGUI>();
        if (num != null)
        {
            num.transform.position = transform.position;
            numText.text = damage.ToString("n0");
            num.SetActive(true);
        }
    }
    protected virtual void Movement()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, 1f);
        agent.SetDestination(target);
    }

    protected virtual void Rotation()
    {
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public enum SpeedChange
    {
        increase, decrease
    }
    protected void ChangeSpeed(float changeAmount, SpeedChange speedChange)
    {
        switch (speedChange)
        {
            case SpeedChange.increase:
                speed += changeAmount;
                break;
            case SpeedChange.decrease:
                speed -= changeAmount;
                break;
        }

        agent.speed = speed;
    }
}
