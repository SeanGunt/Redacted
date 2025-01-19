using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int extraPhysicalDamage;
    public int extraMagicDamage;
    public bool tutorialCompleted;

    public GameData()
    {
        tutorialCompleted = false;
        extraPhysicalDamage = 0;
        extraMagicDamage = 0;
    }
}
