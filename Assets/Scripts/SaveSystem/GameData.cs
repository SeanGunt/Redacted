using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int extraPhysicalDamage;
    public int extraMagicDamage;

    public GameData()
    {
        extraPhysicalDamage = 0;
        extraMagicDamage = 0;
    }
}
