using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrows : MonoBehaviour
{
    [SerializeField] private GameObject shopArrowPrefab;
    private List<Transform> shopArrows = new List<Transform>();
    private float arrowOffsetX = 11.5f;
    private float arrowOffsetY = 6.3f;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject shopArrow = Instantiate(shopArrowPrefab, new Vector3(0f,0f,0f), Quaternion.identity, this.transform);
            shopArrow.name = "ShopArrow_" + i.ToString();
            shopArrows.Add(shopArrow.transform);
        }
    }

    private void Update()
    {
        UpdateShopArrows();
    }
    void UpdateShopArrows()
    {
        for (int i = 0; i < shopArrows.Count; i++)
        {
            Transform assignedShop = MapUpdate.Instance.shopsList[i];
            if (assignedShop != null)
            {
                Vector3 direction = (assignedShop.position - shopArrows[i].position).normalized;

                float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

                shopArrows[i].eulerAngles = new Vector3(0,0,angle);

                Vector3 arrowPos = Camera.main.transform.position + new Vector3(direction.x * arrowOffsetX, direction.y * arrowOffsetY, 0f);

                shopArrows[i].position = new Vector3(arrowPos.x, arrowPos.y, 0f);
                
                float distanceToShop = Vector2.Distance(assignedShop.position, shopArrows[i].position);
                if (distanceToShop <= 11.5f)
                {
                    shopArrows[i].gameObject.SetActive(false);
                }
        
            }
            else
            {
                Debug.Log("Shops Returned Null");
            }
        }
    }
}
