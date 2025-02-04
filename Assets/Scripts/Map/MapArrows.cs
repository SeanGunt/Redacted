using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrows : MonoBehaviour
{
    [SerializeField] private GameObject shopArrowPrefab;
    private List<Transform> shopArrows = new List<Transform>();
    private float arrowOffset = 0.95f;
    private float hideDistance = 3f;

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
        Camera cam = Camera.main;

        for (int i = 0; i < shopArrows.Count; i++)
        {
            Transform assignedShop = MapUpdate.Instance.shopsList[i];
            if (assignedShop != null)
            {
                Vector3 shopScreenPos = cam.WorldToScreenPoint(assignedShop.position);

                if (shopScreenPos.z < 0)
                {
                    shopScreenPos *= -1;
                }

                float distance = Vector3.Distance(shopArrows[i].position, assignedShop.position);

                if (distance < hideDistance)
                {
                    shopArrows[i].gameObject.SetActive(false);
                }
                else
                {
                    shopArrows[i].gameObject.SetActive(true);
                }

                shopScreenPos = new Vector3(
                    Mathf.Clamp(shopScreenPos.x, cam.pixelWidth * (1 - arrowOffset), cam.pixelWidth * arrowOffset),
                    Mathf.Clamp(shopScreenPos.y, cam.pixelHeight * (1 - arrowOffset), cam.pixelHeight * arrowOffset),
                    0
                );

                Vector3 arrowWorldPos = cam.ScreenToWorldPoint(new Vector3(shopScreenPos.x, shopScreenPos.y, cam.nearClipPlane));
                shopArrows[i].position = new Vector3(arrowWorldPos.x, arrowWorldPos.y, 0f);

                Vector3 arrowDirection = assignedShop.position - shopArrows[i].position;
                float angle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;
                shopArrows[i].eulerAngles = new Vector3(0, 0, angle - 90);
            }
            else
            {
                Debug.LogWarning("Assigned shop is null");
            }
        }
    }
}
