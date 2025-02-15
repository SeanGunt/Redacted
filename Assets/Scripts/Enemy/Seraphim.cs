using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Seraphim : EnemyMaster
{
    [Header("Seraphim Specifics")]
    [SerializeField] private GameObject saltBeamIndicatorGO;
    private bool pillarsBeingSpawned;
    private bool enraged;


    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        baseSpeed = speed;

        health = maxHealth;
        enemyID = nextID++;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider2d = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
        MusicManager.instance.FadeTracks(MusicManager.instance.tracks[3], 0f);
    }

    protected override void Update()
    {
        base.Update();
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 12f && !pillarsBeingSpawned)
        {
            StartCoroutine(PillarsOfSalt());
            pillarsBeingSpawned = true;
        }

        if (health <= maxHealth/2)
        {
            enraged = true;
        }
    }

    private IEnumerator PillarsOfSalt()
    {
        float pillarTimer = 1f;
        while (!dead)
        {
            pillarTimer -= Time.deltaTime;
            if (pillarTimer <= 0f)
            {
                Instantiate(saltBeamIndicatorGO, RandomLocationAroundPlayer(), Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
                if (enraged)
                {
                    pillarTimer = 0.5f;
                }
                else
                {
                    pillarTimer = 1f;
                }
            }
            yield return null;
        }
    }

    private Vector2 RandomLocationAroundPlayer()
    {
        float randomY = Random.Range(-2f, 2f);
        float randomX =  Random.Range(-2f, 2f);

        return new Vector2(playerBase.transform.position.x + randomX, playerBase.transform.position.y + randomY);
    }
}
