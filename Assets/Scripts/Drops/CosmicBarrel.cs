using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

[Serializable]
public class BarrelPickups
{
    public GameObject prefab;
    public int chanceToSpawnModifier;
    [HideInInspector] public int spawnChance;
}

public class CosmicBarrel : EnemyMaster
{
    public List<BarrelPickups> pickupsList;
    protected override void Awake()
    {
        health = maxHealth;
        enemyID = nextID++;

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }
    protected override void Update()
    {
        
    }

    protected override IEnumerator Die()
    {
        int randomSpawn = UnityEngine.Random.Range(1, 101); 
        float baseSpawnChance = 100f / pickupsList.Count; //Divide spawn chances evenly for all pickups in list

        int cumulativeChance = 0;
        foreach (BarrelPickups pickup in pickupsList)
        {
            int itemChance = (int)(pickup.chanceToSpawnModifier + baseSpawnChance); //Adds spawn chance modifier to each pickup in list, this is done so items spawn at different chances
            cumulativeChance += itemChance; //Increase cumulative chance for each item in List, this allows for the ranges to be set
            pickup.spawnChance = cumulativeChance; //Set pickup chance to intervals, for example if the the 1st item in the index has a 35% chance to spawn and the 2nd a 25% chance to spawn, set spawnchance of 1st item to 35 and 2nd to 55 (35 + 25)
        }

        //This entire block of code is solely used as a debug. It checks if the intervals are working correctly
        int previousRange = 1;
        foreach (BarrelPickups pickup in pickupsList)
        {
            Debug.Log(pickup.prefab.name + " spawn range: " + previousRange + " to " + pickup.spawnChance);
            previousRange = pickup.spawnChance + 1; 
        }

        //This is neccessary because it ensures that that the last item in the index only goes to 100 so we dont go over which would cause nothing to get spawned
        pickupsList[^1].spawnChance = 100;

        foreach (BarrelPickups pickup in pickupsList)
        {
            if (randomSpawn <= pickup.spawnChance) //The items are spawned by checking to see if the random number generated is less than or equal to the pickups spawn chances. It iterates through all of the items in the pickups
                                                   //For exmaple if the randomSpawn var is 70, it would first check the first index being 35 and !70 <= 35 so it would move on to the next index until it gets to the right item to spawn
            {
                Instantiate(pickup.prefab, transform.position, Quaternion.identity);
                Debug.Log("Number rolled was " + randomSpawn + ", therefore spawning " + pickup.prefab.name);
                break;
            }
        }

        Destroy(gameObject);
        yield return null;
    }

}
