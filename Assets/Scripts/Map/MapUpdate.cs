using System;
using System.ComponentModel;
using System.Data.Common;
using System.Xml.Linq;
using TreeEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapUpdate : MonoBehaviour
{
    // for this to work, tiles must be placed correctly in the inspector
    enum Types
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
        p_c       // pathway-variantC
    }

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
    [SerializeField] public int numRoads;

    private Tilemap baseLayer, collisionLayer, roadLayer;
    private int[,] types;

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

        PerlinToTile();

        GenerateRoads();

        PlaceTiles();
    }

    void PerlinToTile()
    {
        int botEdge, leftEdge, refinedType;
        // topEdge, rightEdge, thisTile,
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
                //topEdge = types[x, y+1];
                leftEdge = types[x-1, y];
                //rightEdge = types[x+1, y];
                //thisTile = types[x, y];
                refinedType = (int)Types.g_c_a;

                // choose a ground variant
                int randomizer = UnityEngine.Random.Range(0, 100);
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
                if (types[x,y] == (int)Types.l)
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

    private void GeneratePerlinGrid()
    {
        types = new int[mapDimensions.x, mapDimensions.y];
        int rng = UnityEngine.Random.Range(0, 10000);
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

    private void GenerateRoads()
    {
        float amplitude = 20f;
        float frequency = 2.0f;
        float phase = 0f;

        for (int y = 10; y < mapDimensions.y-10; y++)
        {
            float angle = y * (2 * Mathf.PI) / mapDimensions.y;
            float x = amplitude * Mathf.Sin(frequency * angle + phase);
            
            Debug.Log((int)x);

            types[(int)x+250, y-1] = (int)Types.p_a;
            types[(int)x+250, y] = (int)Types.p_a;
            types[(int)x+250, y+1] = (int)Types.p_a;
            types[(int)x+250, y+2] = (int)Types.p_a;
            types[(int)x+250, y+3] = (int)Types.p_a;  
        }
        
    }
}

