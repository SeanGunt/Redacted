using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;

enum Tiles
{
    grass = 0,
    water,
    gravel
}

public class MapUpdate : MonoBehaviour
{
    [SerializeField] public TileBase[] tiles;
    [SerializeField] public float noiseScale;
    [SerializeField] public Vector2Int mapDimensions;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float waterMin, waterMax;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float dirtMin, dirtMax;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float grassMin, grassMax;

    private Vector2Int offset;
    private Tilemap baseLayer, collisionLayer;
    private int[,] tileTypes;

    private void Awake()
    {   
        offset.x = mapDimensions.x/2;
        offset.y = mapDimensions.y/2;

        baseLayer = CreateTilemap("base");
        collisionLayer = CreateTilemap("collision");
        collisionLayer.gameObject.AddComponent<TilemapCollider2D>();

        int rand = UnityEngine.Random.Range(0, 10000);
        tileTypes = new int[mapDimensions.x, mapDimensions.y];
        
        baseLayer.ClearAllTiles();
        collisionLayer.ClearAllTiles();

        // Get x by y grid of tiles from perlin noise
        for (int y = 0; y < mapDimensions.y; y++)
        {
            for (int x = 0; x < mapDimensions.x; x++)
            {
                float sampleX = x / noiseScale;
                float sampleY = y / noiseScale;
                
                float perlinValue = Mathf.PerlinNoise(rand + sampleX, rand + sampleY);

                int type;
                if (x != 0 && x != mapDimensions.x-1 && y != 0 && y != mapDimensions.y-1)
                    type = ChooseBaseType(perlinValue);
                else
                    type = (int)Tiles.grass;

                tileTypes[x, y] = type;
            }
        }

        for (int y = 0; y < mapDimensions.y; y++)
        {
            for (int x = 0; x < mapDimensions.x; x++)
            {
                if (x == 0 || x == mapDimensions.x-1 || y == 0 || y == mapDimensions.y-1)
                    continue;
                // this directly modifies the tileTypes[,] array
                ChooseVariantType(x, y);
            }
        }

        for (int y = 0; y < mapDimensions.y; y++)
        {
            for (int x = 0; x < mapDimensions.x; x++)
            {
                PlaceTile(tileTypes[x, y], new Vector3Int(x-offset.x, y-offset.y, 0));
            }
        }
    }

    Tilemap CreateTilemap(string tilemapName)
    {
        GameObject tilemapObject = new(tilemapName);
        tilemapObject.transform.SetParent(transform);

        Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
        tilemapObject.AddComponent<TilemapRenderer>();

        return tilemap;
    }

    private void PlaceTile(int tileType, Vector3Int pos)
    {
        if (tileType == (int)Tiles.water)
        {
            collisionLayer.SetTile(pos, tiles[tileType]);
        }

        baseLayer.SetTile(pos, tiles[tileType]);
    }

    private int ChooseBaseType(float perlinValue)
    {
        int tileType = 0;
        if (perlinValue > grassMin && perlinValue < grassMax) tileType = (int)Tiles.grass;
        if (perlinValue > dirtMin && perlinValue < dirtMax) tileType = (int)Tiles.gravel;
        if (perlinValue > waterMin && perlinValue < waterMax) tileType = (int)Tiles.water;

        return tileType;
    }

    private void ChooseVariantType(int x, int y)
    {   
        /*
        if (tileTypes[x, y] == (int)Tiles.grass)
        {
            // sides
            int leftMostTile = tileTypes[x-1, y];
            int botMostTile = tileTypes[x, y-1];
            int topMostTile = tileTypes[x, y+1];
            int rightMostTile = tileTypes[x+1, y];

            if (leftMostTile == (int)Tiles.water)
                tileTypes[x, y] = (int)Tiles.grass_edge_left;
            if (botMostTile == (int)Tiles.water)
                tileTypes[x, y] = (int)Tiles.grass_edge_bot;
            if (rightMostTile == (int)Tiles.water)
                tileTypes[x, y] = (int)Tiles.grass_edge_right;

            if (topMostTile == (int)Tiles.grass_edge_left && botMostTile == (int)Tiles.water)
                tileTypes[x, y] = (int)Tiles.grass_edge_left;
            if (topMostTile == (int)Tiles.grass_edge_right && botMostTile == (int)Tiles.water)
                tileTypes[x, y] = (int)Tiles.grass_edge_right;
            
        }
        */
    }
}

