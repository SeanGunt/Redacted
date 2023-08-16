using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapUpdate : MonoBehaviour
{
    /* SET FROM INSPECTOR */

    // Density of rocks on the map
    [Range(90, 100)]
    public int rockDensity = 90;

    // The floor and ceiling of the float val that warrants grass
    [Range(0.0f, 1.0f)]
    public float grassFloor = 0.0f;
    [Range(0.0f, 1.0f)]
    public float grassCeiling = 0.0f;
    
    // The floor and ceiling of the float val that warrants dirt
    [Range(0.0f, 1.0f)]
    public float dirtFloor = 0.0f;
    [Range(0.0f, 1.0f)]
    public float dirtCeiling = 0.0f;

    // The floor and ceiling of the float val that warrants water
    [Range(0.0f, 1.0f)]
    public float waterFloor = 0.0f;
    [Range(0.0f, 1.0f)]
    public float waterCeiling = 0.0f;

    // An array of different tiles that we can use
    public TileBase[] tileVariants;
    public Vector3Int playerPos;
    //public Material wavy;

    // These variables are specific to internal functionality
    private Tilemap tmBase, tmCollision;
    readonly private float scale = 20f;
    private float[,] noiseMap;  
    readonly public int width = 500, height = 500;
    private Dictionary<Vector3Int, List<TileBase>> layeredTiles = new Dictionary<Vector3Int, List<TileBase>>();

    // Container for our tiles
    struct Tile 
    {
        public int tileType;
        public Vector3Int tilePos;
    }
    Tile[,] tiles;

    private void Awake()
    {
        // Get the tilemap
        tmBase = CreateTilemap("BaseLayer");
        tmCollision = CreateTilemap("CollisionLayer");

        //tmBase.GetComponent<TilemapRenderer>().material = wavy;
        tmCollision.gameObject.AddComponent<TilemapCollider2D>();

        // Initialize noise map, and tile objects
        noiseMap = new float[width, height];
        tiles = new Tile[width, height];
        int randomness = Random.Range(0, 10000);
        
        // Create the noise map
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(randomness + sampleX, randomness + sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        // Set an offset for placement and clear all the tiles if any are there.
        const int offset = 250;
        int counter = 0;
        bool playerPosFound = false;

        // Now loop and set the type and position of all tiles
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int variant = 0;
                Vector3Int pos = new Vector3Int(x-offset, y-offset, 0);

                if (noiseMap[x, y] > grassFloor && noiseMap[x, y] < grassCeiling) 
                    variant = 1;

                tiles[x, y].tileType = variant;
                tiles[x, y].tilePos = pos;

                if (!playerPosFound && x > 200 && x < 300 && y > 200 && y < 300)
                {
                    if (tiles[x, y].tileType == 1) counter++;

                    if (counter > 5) {
                        playerPos = pos;
                        counter = 0;
                        playerPosFound = true;
                    }
                }

            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || y == 0 || y == height-1 || x == width-1) continue;
                
                int thisTile = tiles[x, y].tileType;
                int leftTile = tiles[x-1, y].tileType;
                int rightTile = tiles[x+1, y].tileType;
                int topTile = tiles[x, y+1].tileType;
                int botTile = tiles[x, y-1].tileType;

                if (thisTile == 1 && leftTile == 0 && topTile == 0 && botTile != 0)
                    tiles[x, y].tileType = 4;
                else if (thisTile == 1 && botTile == 0 && rightTile == 0 && topTile != 0)
                    tiles[x, y].tileType = 3;
                else if (thisTile == 1 && topTile == 0 && rightTile == 0 && botTile != 0)
                    tiles[x, y].tileType = 5;
                else if (thisTile == 1 && leftTile == 0 && botTile == 0 && topTile != 0)
                    tiles[x, y].tileType = 2;
            }
        }
    }

    private void Start()
    {
        tmBase.ClearAllTiles();
        tmCollision.ClearAllTiles();

        // fill with primary tiles
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (tiles[x, y].tileType != 0)
                    tmBase.SetTile(tiles[x, y].tilePos, tileVariants[tiles[x, y].tileType]);
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (tiles[x, y].tileType == 0)
                    tmCollision.SetTile(tiles[x, y].tilePos, tileVariants[tiles[x, y].tileType]);

            }
        }
    }

    Tilemap CreateTilemap(string tilemapName)
    {
        GameObject tilemapObject = new GameObject(tilemapName);
        tilemapObject.transform.SetParent(transform); // Attach to the Grid

        Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
        tilemapObject.AddComponent<TilemapRenderer>();

        return tilemap;
    }
}

