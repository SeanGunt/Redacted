using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyMaster : MonoBehaviour, IDamagable, IFreezable
{
    public float maxHealth, speed, damage;
    [HideInInspector] public float baseSpeed;
    [SerializeField] private int expIndex;
    [SerializeField] protected string deathAnimName;
    protected AnimationClip deathClip;
    public int enemyID;
    protected static int nextID = 0;
    protected float health;
    [SerializeField] private int moneyGainedOnKill;
    [SerializeField] private RectTransform healthBar;
    public Animator animator;
    [HideInInspector] public bool frozen;
    protected bool dead;
    protected bool damagingPlayer;
    protected GameObject player;
    protected PlayerBase playerBase;
    [HideInInspector] public NavMeshAgent agent;
    protected Vector3 target;
    protected SpriteRenderer spriteRenderer;
    protected Material material;
    protected BoxCollider2D collider2d;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip hitAudioClip;
    [SerializeField] protected AudioClip deathAudioClip;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        baseSpeed = speed;

        health = maxHealth;
        enemyID = nextID++;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }

    protected virtual void Start()
    {
        player = GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
        if (agent != null && !agent.isOnNavMesh)
        {
            // This is a bandaid fix, will actually fix this later
            Destroy(gameObject);
        }
    }

    protected virtual void Update()
    {
        if (dead || frozen) return;
        Movement();
        Rotation();
    }

    public void TakeDamage(float damage)
    {
        audioSource.PlayOneShot(hitAudioClip);
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

    public void HandleOnFreeze()
    {
        frozen = true;
        if (agent != null)
        {
            agent.speed = 0;
            agent.isStopped = true;
        }
        if (animator != null)
        {
            animator.speed = 0;
        }
    }

    public void HandleOnUnfreeze()
    {
        frozen = false;
        if (agent != null)
        {
            agent.speed = baseSpeed;
            agent.isStopped = false;
        }
        if (animator != null)
        {
            animator.speed = 1;
        }
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
        agent.isStopped = true;
        collider2d.enabled = false;
        animator.SetTrigger("Die");
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        SFXManager.instance.PlayOneShotAtPoint(transform.position, deathAudioClip);
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == deathAnimName)
            {
                deathClip = clip;
            }
        }
        yield return new WaitForSeconds(deathClip.length);
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
    public void ChangeSpeed(float changeAmount, SpeedChange speedChange)
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

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
}
