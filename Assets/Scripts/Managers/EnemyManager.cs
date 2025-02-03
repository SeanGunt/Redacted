using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public float spawnRate;
    public float spawnRateTimer;
}

[System.Serializable]
public class Wave
{
    public float waveDuration;
    public List<EnemySpawnInfo> enemies = new List<EnemySpawnInfo>();
}

public class EnemyManager : MonoBehaviour, IFreezable
{
    [SerializeField] private List<Wave> waves = new List<Wave>();
    [SerializeField] private Vector2 spawnArea;
    private int unwalkableLayerMask = 1 << 10;
    private int currentWaveIndex = 0;
    private bool frozen;

    private void Awake()
    {

    }

    private void Update()
    {
        if (frozen) return;
        HandleCurrentWave();
        SpawnEnemyWaves();
    }

    private void HandleCurrentWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            return;
        }
        waves[currentWaveIndex].waveDuration -= Time.deltaTime;
        if (waves[currentWaveIndex].waveDuration <= 0)
        {
            currentWaveIndex++;
        }
    }

    private void SpawnEnemyWaves()
    {
        if (currentWaveIndex >= waves.Count)
        {
            return;
        }
        foreach (EnemySpawnInfo enemySpawnInfo in waves[currentWaveIndex].enemies)
        {    
            enemySpawnInfo.spawnRateTimer -= Time.deltaTime;
            if (enemySpawnInfo.spawnRateTimer <= 0)
            {
                GameObject newEnemy = Instantiate(enemySpawnInfo.enemyPrefab, SpawnPosition(), Quaternion.identity);
                newEnemy.transform.SetParent(transform);
                enemySpawnInfo.spawnRateTimer =  enemySpawnInfo.spawnRate;
            }
        }
    }

    private Vector3 SpawnPosition()
    {
        int spawnEdge = Random.Range(0,4);
        Vector3 spawnPosition = Vector3.zero;

        bool validSpawn = false;
        while (!validSpawn)
        {
            switch (spawnEdge)
            {
                case 0:
                    spawnPosition = new Vector3(-20f, Random.Range(-spawnArea.y, spawnArea.y), 0f); 
                    break;
                case 1:
                    spawnPosition = new Vector3(20f, Random.Range(-spawnArea.y, spawnArea.y), 0f);
                    break;
                case 2:
                    spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), -14f, 0f);
                    break;
                case 3:
                    spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 14f, 0f);
                    break;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.1f, unwalkableLayerMask);
            bool insideUnwalkable = false;
            foreach (Collider2D collider in colliders)
            {
                if (!collider.isTrigger)
                {
                    insideUnwalkable = true;
                }
            }

            validSpawn = !insideUnwalkable;

            if (!validSpawn)
            {
                spawnEdge = Random.Range(0,4);
                Debug.Log("Spawn position not valid, trying another position");
            }
        }

        return spawnPosition + GameManager.Instance.player.transform.position;
    }

    public void HandleOnFreeze()
    {
        frozen = true;
    }

    public void HandleOnUnfreeze()
    {
        frozen = false;
    }
}
