using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceballLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float constantSpeed;
    [SerializeField] private float decelerationRate;
    [SerializeField] private Transform[] iceShardSpawnPositions;
    private IceShardLaunch[] iceShardLaunches;
    [SerializeField] private GameObject iceShardPrefab;
    private float iceShardShotRate = 3.0f;
    private float iceShardTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * initialSpeed;
        iceShardTimer =  iceShardShotRate;

        iceShardLaunches = new IceShardLaunch[iceShardSpawnPositions.Length];
    }

    private void Update()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity.normalized * constantSpeed, decelerationRate * Time.deltaTime);
        iceShardTimer -= Time.deltaTime;
        if (iceShardTimer <= 0)
        {
            SpawnShards();
            iceShardTimer = iceShardShotRate;
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
        yield return null;
    }
}
