using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapUpdate : MonoBehaviour
{
    public TileBase[] tileVariants;
    public int[,] map;

    readonly private float scale = 20f;
    private float[,] noiseMap;  
    private Tilemap tilemap;
    readonly public int width = 500, height = 500;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

        noiseMap = new float[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        const int offset = 250;
        // Clear the map (ensures we don't overlap)
        tilemap.ClearAllTiles();
        
        // Loop through the width of the map
        for (int x = 0; x < 500; x++)
        {
            // Loop through the height of the map
            for (int y = 0; y < 500; y++)
            {
                if (noiseMap[x, y] > 0.5)
                    tilemap.SetTile(new Vector3Int(x-offset, y-offset, 0), tileVariants[1]);
                else
                    tilemap.SetTile(new Vector3Int(x-offset, y-offset, 0), tileVariants[0]);
            }
        }
    }
}

