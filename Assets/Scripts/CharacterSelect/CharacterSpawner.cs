using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private int characterIndex;
    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("characterIndex");
        GameObject character = Instantiate(characters[characterIndex], Vector2.zero, Quaternion.identity);
        GameManager.Instance.player = character;
        GameManager.Instance.gameObject.GetComponent<MenusManager>().GetPlayerInput();
    }

}
