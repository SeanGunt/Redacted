using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;

public class MapUpdate : MonoBehaviour
{
    [SerializeField] public TileBase[] Tiles;
    [SerializeField] public float noiseScale;
    [SerializeField] public Vector2Int mapDimensions;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float liquidMin, liquidMax;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float groundMin, groundMax;
    [SerializeField] public int variantTypeProbabilityB, variantTypeProbabilityC;
    [SerializeField] public bool useCustomOffset;
    [SerializeField] public Vector2Int offset;
    [SerializeField] public bool collisionLayerOn;

    private Tilemap baseLayer, collisionLayer, roadLayer;
    private float[,] noiseMap;
    private string[,] map;

    private void Awake()
    {   
        if (!useCustomOffset)
        {
            offset.x = mapDimensions.x/2;
            offset.y = mapDimensions.y/2;   
        } 

        baseLayer = CreateTilemap("base");
        collisionLayer = CreateTilemap("collision");
        roadLayer = CreateTilemap("road");

        if (collisionLayerOn)
        {
            collisionLayer.gameObject.AddComponent<TilemapCollider2D>();
        }
        
        GeneratePerlinGrid();

        GenerateMap();

        PlaceTiles();

        GenerateRoads();
    }

    Tilemap CreateTilemap(string tilemapName)
    {
        GameObject tilemapObject = new(tilemapName);
        tilemapObject.transform.SetParent(transform);

        Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
        tilemapObject.AddComponent<TilemapRenderer>();

        return tilemap;
    }

    private void PlaceTiles()
    {
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                if (map[x,y] == "liquid")
                {
                    collisionLayer.SetTile(new Vector3Int(x-offset.x, y-offset.y, 0), Tiles[0]);
                }
                else
                {
                    baseLayer.SetTile(new Vector3Int(x-offset.x, y-offset.y, 0), Tiles[1]);
                }
            }
        }
    }

    private void GeneratePerlinGrid()
    {
        noiseMap = new float[mapDimensions.x, mapDimensions.y];
        int rng = UnityEngine.Random.Range(0, 10000);
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                float sampleX = x / noiseScale;
                float sampleY = y / noiseScale;
                
                float perlinValue = Mathf.PerlinNoise(rng + sampleX, rng + sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
    }

    private void GenerateMap()
    {
        map = new string[mapDimensions.x, mapDimensions.y];
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                map[x,y] = IsLiquidTile(x,y) ? "liquid" : "ground";
            }
        }
    }

    private void GenerateRoads()
    {
        float amplitude = 20f;
        float frequency = 2.0f;
        float phase = 0f;

        for (int x = 0; x < mapDimensions.x; x++)
        {
            float angle = x * (2 * Mathf.PI) / mapDimensions.x;
            float y = amplitude * Mathf.Sin(frequency * angle + phase);

            roadLayer.SetTile(new Vector3Int(x-offset.x, (int)y, 0), Tiles[7]);
            roadLayer.SetTile(new Vector3Int(x-offset.x, (int)y+1, 0), Tiles[7]);
            roadLayer.SetTile(new Vector3Int(x-offset.x, (int)y-1, 0), Tiles[7]);
        }

        for (int y = 0; y < mapDimensions.y; y++)
        {
            float angle = y * (2 * Mathf.PI) / mapDimensions.y;
            float x = amplitude * Mathf.Sin(frequency * angle + phase);

            roadLayer.SetTile(new Vector3Int((int)x, y-offset.y, 0), Tiles[7]);
            roadLayer.SetTile(new Vector3Int((int)x+1, y-offset.y, 0), Tiles[7]);
            roadLayer.SetTile(new Vector3Int((int)x-1, y-offset.y, 0), Tiles[7]);   
        }
    }

    private bool IsLiquidTile(int x, int y)
    {
        if (noiseMap[x,y] > liquidMin && noiseMap[x,y] < liquidMax)
        {
            return true;
        }
        return false;
    }
}

