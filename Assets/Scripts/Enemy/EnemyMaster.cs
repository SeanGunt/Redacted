using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

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
            playerBase.health -= damage - (damage * Mathf.Log(playerBase.physicalResistance, 10000));
            //Debug.Log(damage * Mathf.Log(playerBase.physicalResistance, 10000));
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
        Destroy(gameObject);
    }

    protected virtual void SpawnDamageNumber(float damage)
    {
        GameObject num = ObjectPool.instance.GetPooledObject(ObjectPool.instance.prefabsToPool[2]);
        TextMeshProUGUI numText =  num.GetComponent<TextMeshProUGUI>();
        if (num != null)
        {
            num.transform.position = transform.position;
            numText.text = damage.ToString();
            num.SetActive(true);
        }
    }
    protected virtual void Movement()
    {
        agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
    }
}
