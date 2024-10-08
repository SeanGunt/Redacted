using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;
using System.Collections.Generic;
using System.Collections;

public class MapUpdate : MonoBehaviour
{
    public static MapUpdate Instance;
    private WorldSingleton world;
    public TileBase[] CollidableTiles;
    public TileBase[] BaseTiles;
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private GameObject ruinsPrefab;
    private Tilemap baseLayer, collidableLayer;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [HideInInspector] public List<Transform> shopsList = new List<Transform>();
    [SerializeField] private Transform worldObjectHolder;
    [SerializeField] private Transform shopsHolder;
    Vector2Int offset;

    void Awake()
    {
        Instance = this;
        world = WorldSingleton.instance;
        
        baseLayer = CreateTilemap("base", 0);
        collidableLayer = CreateTilemap("collidable", 1);

        collidableLayer.gameObject.AddComponent<TilemapCollider2D>();
        collidableLayer.gameObject.layer = 10;

        offset = new Vector2Int(world.mapDimensions.x/2, world.mapDimensions.y/2);
    }

    void Start()
    {
        StartCoroutine(InitializeMap());
    }

    private IEnumerator InitializeMap()
    {
        SpawnShops();
        PlaceTiles();
        yield return new WaitForEndOfFrame();
        SpawnTrees();
        //SpawnRuins();
        navMeshSurface.BuildNavMesh();
    }

    Tilemap CreateTilemap(string tilemapName, int areaType)
    {
        GameObject tilemapObject = new(tilemapName);
        tilemapObject.transform.SetParent(transform);

        NavMeshModifier navMeshModifier = tilemapObject.AddComponent<NavMeshModifier>();
        navMeshModifier.overrideArea = true;
        navMeshModifier.area = areaType;

        Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
        TilemapRenderer tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
        tilemapRenderer.sortingOrder = -1;

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
    }

    private void SpawnShops()
    {
        Vector2Int[] shopPositions = world.GetShopPositions();
        for (int i = 0; i < shopPositions.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(shopPositions[i].x - offset.x, shopPositions[i].y - offset.y, 0);
            GameObject shop = Instantiate(shopPrefab, spawnPosition, Quaternion.identity, shopsHolder);
            shopsList.Add(shop.transform);
        }
    }

    private void SpawnTrees()
    {
        List<Vector2Int> treePositions = world.GetWorldObjectPositionsByIndex(0);
        for (int i = 0; i < treePositions.Count; i++)
        {
            Vector3 spawnPosition = new Vector3(treePositions[i].x, treePositions[i].y, 0);
            int randomTree = Random.Range(0, treePrefabs.Length);
            Instantiate(treePrefabs[randomTree], spawnPosition, Quaternion.identity, worldObjectHolder);
        }
    }

    private void SpawnRuins()
    {
        List<Vector2Int> ruinsPositions = world.GetWorldObjectPositionsByIndex(1);
        for (int i = 0; i < ruinsPositions.Count; i++)
        {
            Vector3 spawnPosition = new Vector3(ruinsPositions[i].x, ruinsPositions[i].y, 0);
            Instantiate (ruinsPrefab, spawnPosition, Quaternion.identity, worldObjectHolder);
        }
    }
}

