using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private Vector3 spawnPosition;
    private int characterIndex;
    private Camera mainCamera;
    private void Awake()
    {
        if (SaveManager.instance._gameData.tutorialCompleted)
        {
            spawnPosition = Vector3.zero;
        }
        else
        {
            spawnPosition = new Vector3(-0.5f, -24f, 0f);
        }
        mainCamera = Camera.main;
        characterIndex = PlayerPrefs.GetInt("characterIndex");
        GameObject character = Instantiate(characters[characterIndex], spawnPosition, Quaternion.identity);
        GameManager.Instance.player = character;
        GameManager.Instance.gameObject.GetComponent<MenusManager>().GetPlayerInput();
        mainCamera.GetUniversalAdditionalCameraData().volumeTrigger = character.transform;
    }

}
