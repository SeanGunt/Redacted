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
    [SerializeField] private SpriteRenderer shadowRenderer;
    private SpriteRenderer spriteRenderer;
    [Header("Iceshard Properties")]
    [SerializeField] private Transform[] iceShardSpawnPositions;
    private IceShardLaunch[] iceShardLaunches;
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

        iceShardLaunches = new IceShardLaunch[iceShardSpawnPositions.Length];
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
            Rigidbody2D rb = iceShard.GetComponent<Rigidbody2D>();
            iceShard.transform.localScale = new Vector2(0f,0f);
            iceShardLaunches[i] = iceShard.GetComponent<IceShardLaunch>();
            StartCoroutine(Expand(iceShard.transform, rb));
        }
    }

    private IEnumerator Expand(Transform iceShard, Rigidbody2D rb)
    {
        while (iceShard.localScale.x <= 1)
        {
            iceShard.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, 0f);
            yield return null;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        for (int i = 0; i < iceShardLaunches.Length; i++)
        {
            iceShardLaunches[i].Launch();
            yield return null;
        }
        if (totalShardClustersShotSinceBirth >= 7)
        {
            yield return new WaitForSeconds(1f);
           while (spriteRenderer.color.a >= 0)
           {
                spriteRenderer.color -= new Color(0f,0f,0f,Time.deltaTime/2);
                shadowRenderer.color -= new Color(0f,0f,0f,Time.deltaTime/2);
                yield return null;
           }
        }
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
