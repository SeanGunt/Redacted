using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Dictionary<string, GameObject> enemyPrefabs = new Dictionary<string, GameObject>{};
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] private TimerManager timerManager;
    private int phase = 1;
    private float timer;

    private void Awake()
    {
        timer = spawnTimer;
        foreach(GameObject enenmy in enemies)
        {
            enemyPrefabs.Add(enenmy.name, enenmy);
        }
    }

    private void Update()
    {
        switch (phase)
        {
            case 1:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    SpawnEnemy("EnemyTest");
                    timer = spawnTimer;
                }
            break;
            case 2:

            break;
        }
    }
    private void SpawnEnemy(string enemyType)
    {

        if (enemyPrefabs.ContainsKey(enemyType))
        {
            GameObject enemyPrefab = enemyPrefabs[enemyType];
            GameObject newEnemy = Instantiate(enemyPrefab, SpawnPosition(), Quaternion.identity);
            newEnemy.transform.SetParent(transform);
        }
        else
        {
            Debug.LogError("Enemy type " + enemyType + " not found!");
        }
    }

    private Vector3 SpawnPosition()
    {
        int spawnArea = Random.Range(0,4);
        Vector3 spawnPosition = Vector3.zero;
        if (spawnArea == 0)
        {
            spawnPosition = SpawnLeft();
        }
        else if (spawnArea == 1)
        {
            spawnPosition = SpawnRight();
        }
        else if (spawnArea == 2)
        {
            spawnPosition = SpawnDown();
        }
        else if (spawnArea == 3)
        {
            spawnPosition = SpawnUp();
        }

        return spawnPosition + GameManager.Instance.player.transform.position;
    }

    private Vector3 SpawnLeft()
    {
        Vector3 positionToSpawn = new Vector3(-20, Random.Range(-spawnArea.y, spawnArea.y), 0);
        return positionToSpawn;
    }

    private Vector3 SpawnRight()
    {
        Vector3 positionToSpawn = new Vector3(20, Random.Range(-spawnArea.y, spawnArea.y), 0);
        return positionToSpawn;
    }

    private Vector3 SpawnDown()
    {
        Vector3 positionToSpawn = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), -14, 0f);
        return positionToSpawn;
    }

    private Vector3 SpawnUp()
    {
        Vector3 positionToSpawn = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 14f, 0f);
        return positionToSpawn;
    }
}
