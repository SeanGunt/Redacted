using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrows : MonoBehaviour
{
    public List<Transform> shopArrows = new List<Transform>();
    private float arrowOffsetX = 11.5f;
    private float arrowOffsetY = 6.3f;

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
        
            }
            else
            {
                Debug.Log("Shops Returned Null");
            }
        }
    }
}
