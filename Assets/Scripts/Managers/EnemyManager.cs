using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }
    /* When checking for position to spawn enemy, we must decide if x = 0, y = (-5.6 - 5.6) and if y = 0, x = (-10, 10)
    We do not want the enemy spawning on screen, they must spawn off screen*/
    private void SpawnEnemy()
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
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPosition + GameManager.Instance.player.transform.position;
        newEnemy.transform.SetParent(transform);
    }

    private Vector3 SpawnLeft()
    {
        Vector3 positionToSpawn = new Vector3(-12, Random.Range(-spawnArea.y, spawnArea.y), 0);
        return positionToSpawn;
    }

    private Vector3 SpawnRight()
    {
        Vector3 positionToSpawn = new Vector3(12, Random.Range(-spawnArea.y, spawnArea.y), 0);
        return positionToSpawn;
    }

    private Vector3 SpawnDown()
    {
        Vector3 positionToSpawn = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), -7, 0f);
        return positionToSpawn;
    }

    private Vector3 SpawnUp()
    {
        Vector3 positionToSpawn = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 7f, 0f);
        return positionToSpawn;
    }
}
