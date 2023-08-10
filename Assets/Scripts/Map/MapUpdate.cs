using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUpdate : MonoBehaviour
{
    bool screenSwitched = true;

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
}
