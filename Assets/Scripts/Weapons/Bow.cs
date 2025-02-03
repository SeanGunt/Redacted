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
    [HideInInspector] public Vector3 mousePos;

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
        if (replicatorPurchased)
        {
            StartCoroutine(ReplicateProjectile(_arrowPrefab, true));
        }
        Utilities.instance.HandleRotation(mousePos, arrow.transform);
    }

    public void SpawnNoCollisionArrow(GameObject _arrowPrefab)
    {
        GameObject arrow = Instantiate(_arrowPrefab, arrowSpawnPoint.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        if (replicatorPurchased)
        {
            StartCoroutine(ReplicateProjectile(_arrowPrefab, false));
        }
        arrow.transform.rotation = weaponRotation.transform.rotation;
    }

    public void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, archer.transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
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

    private IEnumerator ReplicateProjectile(GameObject _arrowPrefab, bool collisionBasedArrow)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject duplicatedArrow = Instantiate(_arrowPrefab, arrowSpawnPoint.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        if (collisionBasedArrow)
        {
            Utilities.instance.HandleRotation(mousePos, duplicatedArrow.transform);
        }
        else
        {
            duplicatedArrow.transform.rotation = weaponRotation.transform.rotation;
        }
    }

    public void Normalize(int index)
    {
        StartCoroutine(ReturnToNormalState(index));
    }

    private IEnumerator ReturnToNormalState(int index)
    {
        yield return new WaitForSeconds(clips[index].length);
        NotInArrowAttack();
        canRotate = true;
        CanFlipSprite();
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
