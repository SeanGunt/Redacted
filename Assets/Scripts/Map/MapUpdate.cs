using System;
using UnityEngine;
using UnityEngine.Tilemaps;

enum LiquidTiles
{
    varA = 0
}

enum GroundTiles
{
    varA = 1,
    varB,
    varC,
    botEdge,
    leftEdge,
    corner
}

enum PathTiles
{
    varA = 7,
    varB,
    varC
}

public class MapUpdate : MonoBehaviour
{
    [SerializeField] public TileBase[] Tiles;
    [SerializeField] public float noiseScale;
    [SerializeField] public Vector2Int mapDimensions;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float liquidMin, liquidMax;
    [Range(-0.2f, 1.2f)]
    [SerializeField] public float groundMin, groundMax;
    [SerializeField] public bool useOffset;
    [SerializeField] public Vector2Int offset;

    private Tilemap baseLayer, collisionLayer;
    private int[,] tileTypes;

    private void Awake()
    {   
        if (!useOffset)
        {
            offset.x = mapDimensions.x/2;
            offset.y = mapDimensions.y/2;   
        } 

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

                int type = (int)GroundTiles.varA;
                if (x != 0 && x != mapDimensions.x-1 && y != 0 && y != mapDimensions.y-1)
                    type = ChooseType(perlinValue, x, y);

                tileTypes[x, y] = type;
            }
        }

        // Place base tiles
        for (int y = 0; y < mapDimensions.y; y++)
        {
            for (int x = 0; x < mapDimensions.x; x++)
            {
                PlaceTile(tileTypes[x, y], new Vector3Int(x-offset.x, y-offset.y, 0));
            }
        }

        // Place roads

        // Place prefab scenery
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
        if (tileType == (int)LiquidTiles.varA)
        {
            collisionLayer.SetTile(pos, Tiles[tileType]);
        }

        baseLayer.SetTile(pos, Tiles[tileType]);
    }

    private int ChooseType(float perlinValue, int Xindex, int Yindex)
    {
        int tileType = 0;
        if (perlinValue > liquidMin && perlinValue < liquidMax)
            tileType = ChooseVariantType((int)LiquidTiles.varA, Xindex, Yindex);
        if (perlinValue > groundMin && perlinValue < groundMax)
            tileType = ChooseVariantType((int)GroundTiles.varA, Xindex, Yindex);

        return tileType;
    }

    private int ChooseVariantType(int tileType, int Xindex, int Yindex)
    {
        int rand = UnityEngine.Random.Range(0, 100);
        switch (tileType)
        {
            case (int)GroundTiles.varA:
                // choose a variant if random number is right
                if (rand > 50)
                {
                    return (int)GroundTiles.varB;
                }
                else if (rand < 30)
                {
                    return (int)GroundTiles.varC;
                }

                // override variant decision with edge blocks if water is near
                if (tileTypes[Xindex-1, Yindex] == (int)LiquidTiles.varA)
                {
                    return (int)GroundTiles.leftEdge;
                }
                else if (tileTypes[Xindex, Yindex-1] == (int)LiquidTiles.varA)
                {
                    return (int)GroundTiles.botEdge;
                }
                else if (tileTypes[Xindex-1, Yindex] == (int)GroundTiles.botEdge &&
                    tileTypes[Xindex, Yindex-1] == (int)GroundTiles.leftEdge)
                {
                    return (int)GroundTiles.corner;
                }

                break;
            case (int)PathTiles.varA:
                // path tile decisions here
                break;
            case (int)LiquidTiles.varA:
                // liquid tile decisions here
                return (int)LiquidTiles.varA;
            default:
                break;
        }
        return (int)GroundTiles.varA;
    }
}

