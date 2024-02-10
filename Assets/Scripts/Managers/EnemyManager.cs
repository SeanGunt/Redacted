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
    private int unwalkableLayerMask = 1 << 10;
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
                    SpawnEnemy("HammerBird");
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
}
