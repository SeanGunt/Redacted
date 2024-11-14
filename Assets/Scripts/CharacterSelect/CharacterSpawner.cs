using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private int characterIndex;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        characterIndex = PlayerPrefs.GetInt("characterIndex");
        GameObject character = Instantiate(characters[characterIndex], Vector2.zero, Quaternion.identity);
        GameManager.Instance.player = character;
        GameManager.Instance.gameObject.GetComponent<MenusManager>().GetPlayerInput();
        mainCamera.GetUniversalAdditionalCameraData().volumeTrigger = character.transform;
    }

}
