using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Dictionary<string, GameObject> enemyPrefabs = new Dictionary<string, GameObject>{};
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private TimerManager timerManager;
    private int phase = 1;
    private int unwalkableLayerMask = 1 << 10;
    private float spawnTimer;
    private float timeToSpawnEnemy;
    private float phaseTimer;
    private float phaseDuration = 60f;

    private void Awake()
    {
        foreach(GameObject enenmy in enemies)
        {
            enemyPrefabs.Add(enenmy.name, enenmy);
        }
        UpdateSpawnSettings();
    }

    private void Update()
    {
        phaseTimer += Time.deltaTime;

        if (phaseTimer >= phaseDuration)
        {
            phase++;
            phaseTimer = 0f;
            UpdateSpawnSettings();
        }
        switch (phase)
        {
            case 1:
                SpawnEnemy("HammerBird");
            break;
            case 2:
                SpawnEnemy("Robot");
            break;
        }
    }
    private void SpawnEnemy(string enemyType)
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawnEnemy)
        {
            if (enemyPrefabs.ContainsKey(enemyType))
            {
                GameObject enemyPrefab = enemyPrefabs[enemyType];
                GameObject newEnemy = Instantiate(enemyPrefab, SpawnPosition(), Quaternion.identity);
                newEnemy.transform.SetParent(transform);
                spawnTimer -= timeToSpawnEnemy;
            }
            else
            {
                Debug.LogError("Enemy type " + enemyType + " not found!");
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
                    spawnPosition = new Vector3(-20, Random.Range(-spawnArea.y, spawnArea.y), 0); 
                    break;
                case 1:
                    spawnPosition = new Vector3(20, Random.Range(-spawnArea.y, spawnArea.y), 0);
                    break;
                case 2:
                    spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), -14, 0f);
                    break;
                case 3:
                    spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 14f, 0f);
                    break;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f, unwalkableLayerMask);
            bool insideUnwalkable = false;
            foreach (Collider2D collider in colliders)
            {
                if (!collider.isTrigger)
                {
                    insideUnwalkable = true;
                    Debug.Log("Spawning Enemy In Walkable Position");
                }
            }

            validSpawn = !insideUnwalkable;

            if (!validSpawn)
            {
                spawnEdge = Random.Range(0,4);
            }
        }

        return spawnPosition + GameManager.Instance.player.transform.position;
    }

    private void UpdateSpawnSettings()
    {
        switch (phase)
        {
            case 1:
                timeToSpawnEnemy = 5f; 
                break;
            case 2:
                timeToSpawnEnemy = 4f;
                break;
        }
    }
}
