using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private readonly Vector2Int[] shopsPos = WorldSingleton.instance.shops;

    struct Shop
    {
        public Vector2Int pos;
        public bool hasBeenVisited;
        public int[] items;
    }

    int numShops = 0;
    Shop[] shops;

    void Awake()
    {
        numShops = WorldSingleton.instance.shopCounter;
        // Initialize list of shops
        shops = new Shop[numShops];

        for (int i = 0; i < numShops; i++)
        {
            shops[i].pos = new Vector2Int(shopsPos[i].x, shopsPos[i].y);
            shops[i].hasBeenVisited = false;
            shops[i].items = new int[10] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        }
        
        DebugPrintShops();
    }

    private void DebugPrintShops()
    {
        for (int i = 0; i < numShops; i++)
        {
            Debug.Log("shop "+i+" pos: "+shops[i].pos);
        }
    }

    
}
