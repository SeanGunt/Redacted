using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem
{
    public string itemName;
    public float health;
    public float healthRegen;
    public float moveSpeed;
    public float physicalDamage;
    public float magicalDamage;
    public float physicalResistance;
    public float magicalResistance;
    public float lifeSteal;
    public float coolDownReduction;
    protected GameObject player;
    protected WeaponBase weaponBase;
    protected PlayerBase playerBase;

    private void Start()
    {
        player = GameManager.Instance.player;
        weaponBase = player.GetComponentInChildren<WeaponBase>();
        playerBase = player.GetComponentInChildren<PlayerBase>();
    }

    public virtual void ActiveAbility()
    {

    }

    public virtual void PassiveAbility()
    {

    }
}
