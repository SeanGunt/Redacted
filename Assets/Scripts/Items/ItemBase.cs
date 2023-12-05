using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem
{
    [Header("Item Stats")]
    public float health;
    public float healthRegen;
    public float moveSpeed;
    public float pickupRange;
    public float physicalDamage;
    public float magicalDamage;
    public float physicalResistance;
    public float magicalResistance;
    public float lifeSteal;
    public float critChance;
    public float coolDownReduction;
    public Sprite imageSprite;
    [Header("Item Information")]
    public string itemName;
    public float activeBaseCooldown;
    private float activeCooldown;

    [Header("References")]
    protected GameObject player;
    protected WeaponBase weaponBase;
    protected PlayerBase playerBase;

    private void Start()
    {
        player = GameManager.Instance.player;
        weaponBase = player.GetComponentInChildren<WeaponBase>();
        playerBase = player.GetComponentInChildren<PlayerBase>();
        AddStats();
    }

    private void AddStats()
    {
        playerBase.health += health;
        playerBase.healthRegen += healthRegen;
        playerBase.speed += moveSpeed;
        playerBase.pickupRange += pickupRange;
        playerBase.physicalDamage += physicalDamage;
        playerBase.magicalDamage += magicalDamage;
        playerBase.physicalResistance += physicalResistance;
        playerBase.magicalResistance += magicalResistance;
        playerBase.lifeSteal += lifeSteal;
        playerBase.critChance += critChance;
        playerBase.cooldownReduction += coolDownReduction;
        playerBase.Invoke("HandleCoolDownReduction", 0f);
    }

    public virtual void ActiveAbility()
    {

    }

    public virtual void PassiveAbility()
    {

    }
}
