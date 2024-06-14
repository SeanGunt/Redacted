using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrowPrefab;
    private WeaponRotation weaponRotation;
    public Archer archer;
    private Vector3 mousePos;

    protected override void Awake()
    {
        base.Awake();
        weaponRotation = GetComponentInParent<WeaponRotation>();
    }
    public void GetMousePosition()
    {
        mousePos = archer.GetMousePosition();
    }
    public void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        Utilities.instance.HandleRotation(mousePos, arrow.transform);
    }

    public void SpawnArrowRain()
    {
        StartCoroutine(HandleArrowRain());
    }

    public void HandleBowAnims(string animName)
    {
        animator.SetTrigger(animName);
    }

    public void RotateBowTowardsSky()
    {
        weaponRotation.transform.eulerAngles = new Vector3(0f,0f,-360f);
    }

    private IEnumerator HandleArrowRain()
    {
        int numOfArrows = 20;
        int arrowsSpawned = 0;
        while (numOfArrows >= arrowsSpawned)
        {
            float randomXOffset = Random.Range(-2.5f, 2.5f);
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(mousePos.x + randomXOffset, transform.position.y + 13.3f), Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
            arrow.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            arrowsSpawned += 1;
            float randomWaitTime = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }
}
