using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrowRPrefab;
    [SerializeField] private GameObject bombPrefab;
    private bool inArrowAttack;
    private WeaponRotation weaponRotation;
    public Archer archer;
    private Vector3 mousePos;

    protected override void Awake()
    {
        base.Awake();
        weaponRotation = GetComponentInParent<WeaponRotation>();
    }

    private void Update()
    {
        HandleWeaponRotation();
    }
    public void GetMousePosition()
    {
        mousePos = archer.GetMousePosition();
    }
    public void SpawnArrow(GameObject _arrowPrefab)
    {
        GameObject arrow = Instantiate(_arrowPrefab, arrowSpawnPoint.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        Utilities.instance.HandleRotation(mousePos, arrow.transform);
    }

    public void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, archer.transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
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

    private void HandleWeaponRotation()
    {
        if (!inArrowAttack) return;
        Vector3 direction = (mousePos - weaponRotation.transform.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        weaponRotation.transform.eulerAngles = new Vector3(0,0,angle);
    }

    private IEnumerator HandleArrowRain()
    {
        int numOfArrows = 20;
        int arrowsSpawned = 0;
        while (numOfArrows >= arrowsSpawned)
        {
            float randomXOffset = Random.Range(-2.5f, 2.5f);
            GameObject arrow = Instantiate(arrowRPrefab, new Vector2(mousePos.x + randomXOffset, transform.position.y + 13.3f), Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
            arrow.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            arrowsSpawned += 1;
            float randomWaitTime = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }

    public void InArrowAttack()
    {
        inArrowAttack = true;
    }

    public void NotInArrowAttack()
    {
        inArrowAttack = false;
    }
}
