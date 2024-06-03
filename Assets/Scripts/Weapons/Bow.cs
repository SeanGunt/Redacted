using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrowPrefab;
    public Archer archer;
    private Vector3 mousePos;

    public void GetMousePosition()
    {
        mousePos = archer.GetMousePosition();
    }
    public void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        Utilities.instance.HandleRotation(mousePos, arrow.transform);
    }

    public void HandleBowAnims(string animName)
    {
        animator.SetTrigger(animName);
    }
}
