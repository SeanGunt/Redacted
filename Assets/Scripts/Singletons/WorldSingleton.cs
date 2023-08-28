using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class WorldSingleton : MonoBehaviour
{
    public enum Types
    {
        l = 0,    // liquid
        g_c_a,    // ground-center-variantA
        g_c_b,    // ground-center-varaintB
        g_c_c,    // ground-genter-variantC
        g_b_e,    // ground-bot-edge
        g_l_e,    // ground-left-edge
        g_b_l_c,  // ground-bot-left-corner
        p_a,      // pathway-variantA
        p_b,      // pathway-variantB
        p_c,      // pathway-variantC
        shop,     // shop
    }

    public static WorldSingleton instance;
    public int[,] types;
    bool[] activeShops;

    readonly private int numShops = 10;
    readonly private float noiseScale = 20;
    public Vector2Int mapDimensions = new(1000, 1000);
    readonly private float liquidMin = -0.2f, liquidMax = 0.10f;
    readonly private int variantTypeProbabilityB = 5, variantTypeProbabilityC = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }     

        types = null;

        activeShops = new bool[numShops];
        for (int i = 0; i < numShops; i++)
        {
            activeShops[i] = true;
        }


        // implement a loading bar
        GeneratePerlinGrid();

        PerlinToTile();

        GenerateRoads();

        GenerateShops();
    }

    private void GeneratePerlinGrid()
    {
        types = new int[mapDimensions.x, mapDimensions.y];
        int rng = Random.Range(0, 10000);
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                float sampleX = x / noiseScale;
                float sampleY = y / noiseScale;
                
                float perlinValue = Mathf.PerlinNoise(rng + sampleX, rng + sampleY);

                if (perlinValue > liquidMin && perlinValue < liquidMax)
                {
                    types[x, y] = (int)Types.l;
                }
                else
                {
                    types[x, y] = (int)Types.g_c_a; 
                }
            }
        }
    }

    void PerlinToTile()
    {
        int botEdge, leftEdge, refinedType;
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                if (x == 0 || x == mapDimensions.x || y == 0 || y == mapDimensions.y
                    || types[x, y] == (int)Types.l)
                {
                    continue;
                }

                botEdge = types[x, y-1];
                leftEdge = types[x-1, y];
                refinedType = (int)Types.g_c_a;

                // choose a ground variant
                int randomizer = Random.Range(0, 100);
                if (randomizer < variantTypeProbabilityB)
                {
                    refinedType = (int)Types.g_c_b;
                }
                else if (randomizer < variantTypeProbabilityC)
                {
                    refinedType = (int)Types.g_c_c;
                }

                // now choose an edge case
                if (botEdge == (int)Types.l)
                {
                    refinedType = (int)Types.g_b_e;
                }
                else if (leftEdge == (int)Types.l)
                {
                    refinedType = (int)Types.g_l_e;
                }
                else if (leftEdge == (int)Types.g_b_e)
                {
                    refinedType = (int)Types.g_b_l_c;
                }

                types[x, y] = refinedType;
            }
        }
    }
    
    private void GenerateRoads()
    {
        float amplitude = 20f;
        float frequency = 2.0f;
        float phase = 0f;

        for (int y = 10; y < mapDimensions.y-10; y++)
        {
            float angle = y * (2 * Mathf.PI) / mapDimensions.y;
            float x = amplitude * Mathf.Sin(frequency * angle + phase);

            types[(int)x+250, y-1] = (int)Types.p_a;
            types[(int)x+250, y] = (int)Types.p_a;
            types[(int)x+250, y+1] = (int)Types.p_a;
            types[(int)(x+250)-1, y] = (int)Types.p_a;
            types[(int)(x+250), y] = (int)Types.p_a;
            types[(int)(x+250)+1, y] = (int)Types.p_a;
        }
    }

    private void GenerateShops()
    {
        /* will use these later

        bool chunkHasShop = false;
        float numerator = 1.0f;
        float denominator = 4.0f;
        */

        for (int x = 5; x < mapDimensions.x; x++)
        {
            for (int y = 5; y < mapDimensions.y; y++)
            {
                // logic for placing the shops goes here

                // modify types[,]
            }
        }
    }

    private bool ValidAreaForShop(int x, int y)
    {
        if (types[x,y] != (int)Types.g_c_a) return false;

        int grass = (int)Types.g_c_a;
        // Check if the types along the horizontal and vertical axis are grass
        if (
            types[x-3,y] == grass && types[x-2,y] == grass &&
            types[x-1,y] == grass && types[x+1,y] == grass &&
            types[x+2,y] == grass && types[x+3,y] == grass &&
            types[x,y-3] == grass && types[x,y-2] == grass &&
            types[x,y-1] == grass && types[x,y+1] == grass &&
            types[x,y+2] == grass && types[x,y+3] == grass
        ) {
            return true;
        }

        return false;
    }
}
