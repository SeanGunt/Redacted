using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public void SetCharacterIndex(int index)
    {
        PlayerPrefs.SetInt("characterIndex", index);
        PlayerPrefs.Save();
    }
}
