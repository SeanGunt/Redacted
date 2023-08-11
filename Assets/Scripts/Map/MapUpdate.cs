using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapUpdate : MonoBehaviour
{
    public TileBase[] tileVariants; // An array of your Tile Assets

    private Tilemap tilemap;

    public int tilemapHeight = 500, tilemapWidth = 500;
    public int[,] map;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();

        map = new int[tilemapWidth, tilemapHeight];
        
        if (map != null) GenerateLayout(ref map);
    }

    private void Start()
    {
        RenderMap(map, tilemap, tileVariants);
    }

    private void Update()
    {
        // If screen has switched call GenerateLayout and then RenderMap
    }

    private void GenerateLayout(ref int[,] map)
    {
        for (int x = 0; x < tilemapWidth; x++)
        {
            for (int y = 0; y < tilemapHeight; y++)
            {
                // We are placing a grass block in every spot here for now
                // soon we will have code here to randomly place trees, shops,
                // roads, bushes, street lamps, stone statues, etc...
                //
                // The map is a 2D array of 1s for now, but we can use this
                // loop to procedurally put numbers 1 - x where x is the number
                // of possible tiles.
                //
                // I would like to have functions for each kind of object we can place
                // they need to be able to select a different kind of block depending
                // on the level so they shouldn't be too world specific... For instance,
                // a palm tree instead of a pine tree.
                //
                // We can use a noise generation function to achieve grass and roads.
                // We can find some kind of noise that looks similar to how trees would
                // placed there. Then we can just follow where the roads are to place
                // street lamps.
                map[x,y] = 1;
            }
        }
    }

    private void RenderMap(int[,] map, Tilemap tilemap, TileBase[] tile)
    {
        const int offset = 250;
        // Clear the map (ensures we don't overlap)
        tilemap.ClearAllTiles();
        
        // Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            // Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x-offset, y-offset, 0), tile[0]);
                }
            }
        }
    }

/*
    bool screenSwitched = true;
    [SerializeField] TileBase[] tiles;

    void Awake()
    {
        // Get stuff ready here
    }

    // Update is called once per frame
    void Update()
    {
        if (screenSwitched)
        {
            PlaceTiles();
            screenSwitched = false;
        }
    }

    void GenerateGrid()
    {
        // TileBase[] tb = new TileBase[2];
        // Resources.Load<TileBase>("Tilemaps/Tiles/level_" + level + "/tile_" + i);
        Debug.Log("GenerateGrid() called");
    }

    void PlaceTiles()
    {
        int xAxis, yAxis;
        for (xAxis = 0; xAxis < 10; xAxis++)
        {
            for (yAxis = 0; yAxis < 10; yAxis++)
            {
                // Put code here to place tiles from tile array
                Debug.Log("PlaceTiles() called");
            }
        }
    }
*/
}
