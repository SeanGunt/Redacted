using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;


[System.Serializable]
public class WorldObjects
{
    [HideInInspector] public List<Vector2Int> worldPositions;
    public int amountOfObjects;
}

public class WorldSingleton : MonoBehaviour
{
    public enum Base
    {
        b_v_a = 0,      // base_variant_A
        b_v_b,          // base_variant_B
        b_v_c,          // base_variant_C
        b_v_d,          // base variant_D
        b_v_e,          // base variant_E
        b_v_f,          // base variant_F
        b_v_g,           // base varient_G
        b_v_h
    }
    public enum Collidable
    {
        c_v_a = 0,      // collidable_variant_A
    }

    public static WorldSingleton instance;
    public int[,] bases;
    public int[,] collidables;

    readonly public Vector2Int mapDimensions = new(500, 500);
    private Vector2Int[] shopPositions = new Vector2Int[3];
    public List<WorldObjects> worldObjects = new();
    readonly private float collidableMin = -0.2f, collidableMax = 0.25f;
    readonly private float noiseScale = 20;
    readonly private int variantProbability = 35;
    private int basesLength;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }     

        bases = null;
        collidables = null;
        basesLength = Enum.GetNames(typeof(Base)).Length;
    }

    void Start()
    {
        StartCoroutine(InitializePositions());
    }

    private IEnumerator InitializePositions()
    {
        GenerateBaseAndCollidableGrid();
        AddNoCollideSpawnZone();
        yield return new WaitForEndOfFrame();
        GenerateShopPositions();
        GenerateWorldObjects();
    }

    private void GenerateBaseAndCollidableGrid()
    {
        bases = new int[mapDimensions.x, mapDimensions.y];
        collidables = new int[mapDimensions.x, mapDimensions.y];
        int rng = UnityEngine.Random.Range(0, 10000);
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                float sampleX = x / noiseScale;
                float sampleY = y / noiseScale;
                
                float perlinValue = Mathf.PerlinNoise(rng + sampleX, rng + sampleY);

                int baseVariant = UnityEngine.Random.Range(0, basesLength);
                bool addVariant = UnityEngine.Random.Range(0, 100) < variantProbability;

                if (perlinValue > collidableMin && perlinValue < collidableMax)
                {
                    collidables[x, y] = (int)Collidable.c_v_a;

                    bases[x, y] = -1;
                }
                else
                {
                    bases[x, y] = (int)Base.b_v_a + (addVariant ? baseVariant : 0);
                    collidables[x, y] = -1;
                }
            }
        }
    }

    private void GenerateShopPositions()
    {
        int offsetX = mapDimensions.x / 4;
        int offsetY = mapDimensions.y / 4;

        List<int> cornerIndices = new() {1,2,3,4};
        cornerIndices = cornerIndices.OrderBy(x => UnityEngine.Random.value).ToList();

        int minDistanceFromEdge = 20;

        for (int i = 0; i < 3; i++)
        {
            int cornerIndex = cornerIndices[i];

            while(true)
            {
                switch (cornerIndex)
                {
                    case 0:
                        shopPositions[i] = new Vector2Int(offsetX, offsetY);
                        break;
                    case 1:
                        shopPositions[i] = new Vector2Int(mapDimensions.x - offsetX, offsetY);
                        break;
                    case 2:
                        shopPositions[i] = new Vector2Int(offsetX, mapDimensions.y - offsetY);
                        break;
                    case 3:
                        shopPositions[i] = new Vector2Int(mapDimensions.x - offsetX, mapDimensions.y - offsetY);
                        break;
                    default:
                        shopPositions[i] = Vector2Int.zero;
                        break;
                }

                int cornerOffsetX = UnityEngine.Random.Range(-offsetX / 2, offsetX / 2);
                int cornerOffsetY = UnityEngine.Random.Range(-offsetY / 2, offsetY / 2);

                shopPositions[i] += new Vector2Int(cornerOffsetX, cornerOffsetY);

                if (shopPositions[i].x >= minDistanceFromEdge &&
                shopPositions[i].x < mapDimensions.x - minDistanceFromEdge &&
                shopPositions[i].y >= minDistanceFromEdge &&
                shopPositions[i].y < mapDimensions.y - minDistanceFromEdge &&
                IsNonCollidableTile(shopPositions[i]))
                {
                    break;
                }
            }

        }
        
    }

    private void GenerateWorldObjects()
    {
        for (int i = 0; i < worldObjects.Count; i++)
        {
            if (worldObjects[i].worldPositions == null || worldObjects[i].worldPositions.Count != worldObjects[i].amountOfObjects)
            {
                Debug.Log("Initializing worldPositions for worldObject " + i + " with size " + worldObjects[i].amountOfObjects);
                worldObjects[i].worldPositions = new List<Vector2Int>(new Vector2Int[worldObjects[i].amountOfObjects]);
            }

            for (int j = 0; j < worldObjects[i].amountOfObjects; j++)
            {
                while (true)
                {
                    int randomX = UnityEngine.Random.Range(-245, 245);
                    int randomY = UnityEngine.Random.Range(-245, 245);
                    Vector2Int randomPosition = new Vector2Int(randomX, randomY);

                    int arrayX = randomX + (mapDimensions.x / 2);
                    int arrayY = randomY + (mapDimensions.y / 2);

                    if (arrayX >= 0 && arrayX < mapDimensions.x && arrayY >= 0 && arrayY < mapDimensions.y)
                    {
                        if (collidables[arrayX, arrayY] == -1)
                        {
                            worldObjects[i].worldPositions[j] = randomPosition;
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Generated position out of bounds: " + randomPosition);
                    }
                }
            }
        }
    }


    private bool IsWorldObjectPlacementValid(Vector2Int position)
    {
        for (int i = 0; i < worldObjects.Count; i++)
        {
            for (int j = 0; j < worldObjects[i].amountOfObjects; j++)
            {
                if (worldObjects[i].worldPositions[j] != Vector2Int.zero && Vector2Int.Distance(worldObjects[i].worldPositions[j], position) < 5)
                {
                    Debug.Log("Objects spawned too close, moving to a different position");
                    return false; 
                }
            }
        }
        return true;
    }

    public Vector2Int[] GetShopPositions()
    {
        return shopPositions;
    }

    public List<Vector2Int> GetWorldObjectPositionsByIndex(int index)
    {
        return worldObjects[index].worldPositions;
    }

    private bool IsNonCollidableTile(Vector2Int position)
    {
        if (position.x < 0 || position.x >= mapDimensions.x || position.y < 0 || position.y >= mapDimensions.y)
        return false;

        return bases[position.x, position.y] != -1;
    }

    public void DebugThis()
    {
        Debug.Log("Check");
    }

    void AddNoCollideSpawnZone()
    {
        int leftX = (mapDimensions.x/2)-6;
        int rightX = (mapDimensions.x/2)+6;
        int topY = (mapDimensions.y/2)-6;
        int botY = (mapDimensions.y/2)+6;
        for (int x = leftX; x < rightX; x++) 
        {
            for (int y = topY; y < botY; y++) 
            {
                int baseVariant = UnityEngine.Random.Range(0, basesLength);
                bool addVariant = UnityEngine.Random.Range(0, 100) < variantProbability;
                bases[x, y] = (int)Base.b_v_a + (addVariant ? baseVariant : 0);
            }
        }
    }
}
