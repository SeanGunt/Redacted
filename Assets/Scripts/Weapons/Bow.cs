using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Archer archer;
    [SerializeField] private Transform rotationPoint;
    private Vector3 mousePos;

    public void GetMousePosition()
    {
        mousePos = archer.GetMousePosition();
    }
    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        arrow.transform.eulerAngles = rotationPoint.eulerAngles;
    }
}
