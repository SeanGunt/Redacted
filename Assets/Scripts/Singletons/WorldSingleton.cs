using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

/*
    WORLD GENERATION AND HOW TO USE THIS SCRIPT:

    This script has the very important role of setting the initial positions of
    world tiles and structure tiles. This is done using Perlin noise. For any map,
    we define a few tile types that will not change.

    Collidable types:

    - collision-variant-<A, B, C> 
        (most prominent collidable type such as water)
    - natural-variant-<A, B, C> 
        (any object such as a rock or a tree)
    - structural-variant-<A, B, C>
        (any object such as a fire, or lightpost)

    Base types:

    - base-variant-<A, B, C>
        (most prominent non-collidable type such as grass or brick)

    As you can see there can be either an A, B, or C variant of any particular tile
    for any level. This will allow us to store folders containing certain sprites for
    particular levels, and dynamically load the right assets at run-time WHILE still
    being able to make the map look interesting procedurally generated.

    You can find all the variables that dictate how a map is generated inside this
    script. They must be adjusted from this script.
*/

public class WorldSingleton : MonoBehaviour
{
    // Tiles that are non-collidable and provide a base to the map
    public enum Base
    {
        b_v_a = 0,      // base_variant_A
        b_v_b,          // base_variant_B
        b_v_c,          // base_variant_C
        b_e_b,          // base_edge_bottom
        b_e_l,          // base_edge_left
        b_c,            // base_corner
        b_i_c           // base_inverted_corner
    }

    // Tiles that are collidable AND provide a base to the map
    public enum Collidable
    {
        c_v_a = 0,      // collidable_variant_A
        c_v_b,          // collidable_variant_B
        c_v_c           // collidable_variant_C
    }

    // Tiles that are collidable
    public enum Object
    {
        n_v_a = 0,      // natural_variant_A
        n_v_b,          // natural_variant_B
        n_v_c,          // natural_variant_C
        s_v_a,          // structural_variant_A
        s_v_b,          // structural_variant_B
        s_v_c           // structural_variant_C
    }

    public static WorldSingleton instance;
    public int[,] bases;
    public int[,] collidables;

    readonly public Vector2Int mapDimensions = new(500, 500);
    readonly private float collidableMin = -0.2f, collidableMax = 0.25f;
    readonly private float noiseScale = 20;
    readonly private int variantProbability = 20;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }     

        bases = null;
        collidables = null;
    }

    void Start()
    {
        GenerateBaseAndCollidableGrid();
        AddEdgePieces();
        AddNoCollideSpawnZone();
    }

    private void GenerateBaseAndCollidableGrid()
    {
        bases = new int[mapDimensions.x, mapDimensions.y];
        collidables = new int[mapDimensions.x, mapDimensions.y];
        int rng = Random.Range(0, 10000);
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                float sampleX = x / noiseScale;
                float sampleY = y / noiseScale;
                
                float perlinValue = Mathf.PerlinNoise(rng + sampleX, rng + sampleY);

                int variant = Random.Range(0, 3);
                bool addVariant = Random.Range(0, 100) < variantProbability ? true : false;

                if (perlinValue > collidableMin && perlinValue < collidableMax)
                {
                    collidables[x, y] = (int)Collidable.c_v_a + (addVariant ? variant : 0);
                    bases[x, y] = -1;
                }
                else
                {
                    bases[x, y] = (int)Base.b_v_a + (addVariant ? variant : 0);
                    collidables[x, y] = -1;
                }
            }
        }
    }

    void AddEdgePieces()
    {
        for (int x = 0; x < mapDimensions.x; x++)
        {
            for (int y = 0; y < mapDimensions.y; y++)
            {
                // Skip this iteration if the following are true
                if (bases[x,y] == -1) continue;
                if (x == 0 || x == mapDimensions.x-1 || y == 0 || y == mapDimensions.y-1) continue;

                // if left-most tile is a collidable and bot-most tile is collidable
                if (bases[x-1, y] == -1 && bases[x, y-1] == -1)
                {
                    bases[x, y] = (int)Base.b_i_c;
                }

                // if left-most tile is not collidable and bot-most tile is collidable
                if (bases[x-1, y] != -1 && bases[x, y-1] == -1)
                {
                    bases[x, y] = (int)Base.b_e_b;
                }

                // if left most tile is collidable and bot-most tile is collidable
                if (bases[x-1, y] == -1 && bases[x, y-1] != -1)
                {
                    bases[x, y] = (int)Base.b_e_l;
                }

                // if bot-left-corner tile is collidable and the left-most and bot-most tiles are non-collidable
                if (bases[x-1, y-1] == -1 && bases[x-1, y] != -1 && bases[x, y-1] != -1)
                {
                    bases[x, y] = (int)Base.b_c;
                }
            }
        }
    }

    void AddNoCollideSpawnZone()
    {
        int leftX = (mapDimensions.x/2)-6;
        int rightX = (mapDimensions.x/2)+6;
        int topY = (mapDimensions.y/2)-6;
        int botY = (mapDimensions.y/2)+6;
        for (int x = leftX; x < rightX; x++) {
            for (int y = topY; y < botY; y++) {
                bases[x, y] = (int)Base.b_v_a;
            }
        }
    }
}
