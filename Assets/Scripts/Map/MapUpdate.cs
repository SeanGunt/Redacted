using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;

public class MapUpdate : MonoBehaviour
{
    private WorldSingleton world;
    public TileBase[] CollidableTiles;
    public TileBase[] BaseTiles;
    [SerializeField] private GameObject shopPrefab;
    private Tilemap baseLayer, collidableLayer;
    [SerializeField] private NavMeshSurface navMeshSurface;
    Vector2Int offset;

    void Awake()
    {
        world = WorldSingleton.instance;
        
        baseLayer = CreateTilemap("base", 0);
        collidableLayer = CreateTilemap("collidable", 1);

        collidableLayer.gameObject.AddComponent<TilemapCollider2D>();
        collidableLayer.gameObject.layer = 10;

        offset = new Vector2Int(world.mapDimensions.x/2, world.mapDimensions.y/2);
    }

    void Start()
    {
        SpawnShops();
        PlaceTiles();
    }

    Tilemap CreateTilemap(string tilemapName, int areaType)
    {
        GameObject tilemapObject = new(tilemapName);
        tilemapObject.transform.SetParent(transform);

        NavMeshModifier navMeshModifier = tilemapObject.AddComponent<NavMeshModifier>();
        navMeshModifier.overrideArea = true;
        navMeshModifier.area = areaType;

        Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
        tilemapObject.AddComponent<TilemapRenderer>();

        return tilemap;
    }

    private void PlaceTiles()
    {
        for (int x = 0; x < world.mapDimensions.x; x++)
        {
            for (int y = 0; y < world.mapDimensions.y; y++)
            {
                if (world.bases[x,y] == -1) 
                {
                    collidableLayer.SetTile(
                        new Vector3Int(x-offset.x, y-offset.y, 0),
                        CollidableTiles[world.collidables[x,y]]
                    );
                }
                else // it is a non-collidable tile
                {
                    baseLayer.SetTile(
                        new Vector3Int(x-offset.x, y-offset.y, 0),
                        BaseTiles[world.bases[x,y]]
                    );
                }
            }
        }

        navMeshSurface.BuildNavMesh();
    }

    private void SpawnShops()
    {
        Vector2Int[] shopPositions = world.GetShopPositions();
        for (int i = 0; i < shopPositions.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(shopPositions[i].x - offset.x, shopPositions[i].y - offset.y, 0);
            GameObject shop = Instantiate(shopPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

