using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyMaster : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth, speed, damage;
    [SerializeField] private int expIndex;
    public int enemyID;
    protected static int nextID = 0;
    protected float health;
    [SerializeField] private int moneyGainedOnKill;
    [SerializeField] private RectTransform healthBar;
    protected bool dead;
    protected bool damagingPlayer;
    protected GameObject player;
    protected PlayerBase playerBase;
    protected NavMeshAgent agent;
    protected Vector3 target;
    protected SpriteRenderer spriteRenderer;
    protected Material material;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;

        health = maxHealth;
        enemyID = nextID++;

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }

    protected virtual void Start()
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
        if (healthBar != null)
        {
            healthBar.sizeDelta -= new Vector2(damageToBar, 0);
        }
        SpawnDamageNumber(damage);

        if (health <= 0 && !dead)
        {
            StartCoroutine(Die());
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            damagingPlayer = true;
            StartCoroutine(DoDamage());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            damagingPlayer = false;
        }
    }

    protected virtual IEnumerator Die()
    {
        dead = true;
        GameObject exp = ObjectPool.instance.GetPooledObject(ObjectPool.instance.listOfPooledObjects[1].prefabsToPool[expIndex].prefab);
        if (exp != null)
        {
            exp.transform.position = transform.position;
            exp.SetActive(true);
        }
        MoneyManager.instance.AddMoney(moneyGainedOnKill);
        Destroy(gameObject);
        yield return null;
    }

    protected virtual IEnumerator DoDamage()
    {
        float damageInterval = 0f;
        while (damagingPlayer)
        {
            damageInterval -= Time.deltaTime;
            if (damageInterval <= 0)
            {
                playerBase.TakeDamage(damage, Mathf.Log(playerBase.physicalResistance, 10000));
                damageInterval = 0.2f;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

    protected virtual void SpawnDamageNumber(float damage)
    {
        GameObject num = ObjectPool.instance.GetPooledObject(ObjectPool.instance.listOfPooledObjects[2].prefabsToPool[0].prefab);
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
