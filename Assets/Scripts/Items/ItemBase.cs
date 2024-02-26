using System.Collections;
using UnityEngine.UI;
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
    protected float activeCooldown;
    protected Image cooldownImage;

    [Header("References")]
    protected GameObject player;
    protected WeaponBase weaponBase;
    protected PlayerBase playerBase;
    

    private void Start()
    {
        player = GameManager.Instance.player;
        weaponBase = player.GetComponentInChildren<WeaponBase>();
        playerBase = player.GetComponentInChildren<PlayerBase>();
        cooldownImage = GetComponent<Image>();
        AddStats();
    }

    private void AddStats()
    {
        playerBase.health += health;
        playerBase.baseHealth += health;
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

    protected virtual IEnumerator HandleItemCooldown()
    {
        activeCooldown = activeBaseCooldown;
        cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.b, cooldownImage.color.r, 0.9f);
        cooldownImage.fillAmount = 1f;
        while (activeCooldown >= 0)
        {
            activeCooldown -= Time.deltaTime;
            cooldownImage.fillAmount -= Time.deltaTime / activeBaseCooldown;
            yield return null;
        }
    }
}
