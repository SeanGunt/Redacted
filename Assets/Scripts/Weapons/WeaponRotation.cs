using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private PlayerBase playerBase;
    private WeaponBase weaponBase;

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        weaponBase = GetComponentInChildren<WeaponBase>();
    }
    
    void Update()
    {
        HandleWeaponRotation();
    }
    private void HandleWeaponRotation()
    {
        if (!weaponBase.canRotate) return;
        Vector3 direction = (playerBase.GetMousePosition() - transform.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }
}
