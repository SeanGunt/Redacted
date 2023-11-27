using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
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
}
