using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapUpdate : MonoBehaviour
{
    // for this to work, tiles must be placed correctly in the inspector
    private WorldSingleton world;

    public TileBase[] Tiles;

    private Tilemap baseLayer, collisionLayer, roadLayer;
    private int[,] types;
    Vector2Int mapDimensions;
    Vector2Int offset;

    void Start()
    {
        world = WorldSingleton.instance;
        
        // for new script
        baseLayer = CreateTilemap("base");
        collisionLayer = CreateTilemap("collision");
        roadLayer = CreateTilemap("road");

        collisionLayer.gameObject.AddComponent<TilemapCollider2D>();

        mapDimensions = world.mapDimensions;
        offset = new Vector2Int(mapDimensions.x/2, mapDimensions.y/2);

        types = world.types;

        PlaceTiles();
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
                if (types[x,y] == 0)
                {
                    collisionLayer.SetTile(new Vector3Int(x-offset.x, y-offset.y, 0), Tiles[types[x,y]]);
                }
                else
                {
                    baseLayer.SetTile(new Vector3Int(x-offset.x, y-offset.y, 0), Tiles[types[x,y]]);
                }
            }
        }
    }
}

