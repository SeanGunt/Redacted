using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceballLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Iceball Properties")]
    [SerializeField] private float initialSpeed;
    [SerializeField] private float constantSpeed;
    [SerializeField] private float decelerationRate;
    [SerializeField] private float initialRotationSpeed;
    [SerializeField] private SpriteRenderer shadowSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    private bool fadeStarted;
    [Header("Iceshard Properties")]
    [SerializeField] private Transform[] iceShardSpawnPositions;
    [SerializeField] private GameObject iceShardPrefab;
    [SerializeField] private float iceShardShotRate;
    private float iceShardTimer;
    private int totalShardClustersShotSinceBirth = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.velocity = transform.up * initialSpeed;
        iceShardTimer =  iceShardShotRate;
        rb.AddTorque(initialRotationSpeed);
    }

    private void Update()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity.normalized * constantSpeed, decelerationRate * Time.deltaTime);
        
        iceShardTimer -= Time.deltaTime;

        if (CanShootIceShards())
        {
            SpawnShards();
            totalShardClustersShotSinceBirth += 1;
            iceShardTimer = iceShardShotRate;
        }

        if (CanStartFade())
        {
            StartCoroutine(Fade());
            fadeStarted = true;
        }

        if (IceballHasFaded())
        {
            Destroy(gameObject);
        }
    }

    private void SpawnShards()
    {
        for (int i = 0; i < iceShardSpawnPositions.Length; i++)
        {
            GameObject iceShard = Instantiate(iceShardPrefab, iceShardSpawnPositions[i].transform.position, iceShardSpawnPositions[i].transform.rotation, transform);
        }
    }

    private IEnumerator Fade()
    {
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color -= new Color(0,0,0, Time.deltaTime/3);
            shadowSpriteRenderer.color -= new Color(0,0,0, Time.deltaTime/3);
            yield return null;
        }
    }

    private bool CanStartFade()
    {
        return totalShardClustersShotSinceBirth >= 7 && !fadeStarted;
    }
    private bool CanShootIceShards()
    {
        return iceShardTimer <= 0 && totalShardClustersShotSinceBirth < 7;
    }

    private bool IceballHasFaded()
    {
        return spriteRenderer.color.a <= 0;
    }
}
