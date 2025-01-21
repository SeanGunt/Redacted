using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterSelector : MonoBehaviour, IDistanceInteractable
{
    [SerializeField] private int index;
    [SerializeField] private GameObject characterToSwap;
    [SerializeField] private GameObject interactCanvas;
    [SerializeField] private ShopManager shopManager;
    private Camera mainCamera;

    private void Awake()
    {
        interactCanvas.SetActive(false);
    }
    public void SetCharacterIndex()
    {
        PlayerPrefs.SetInt("characterIndex", index);
        PlayerPrefs.Save();
        Debug.Log("Set Index");
    }

    public void SwapCharacter()
    {
        mainCamera = Camera.main;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.transform;
        Destroy(player);
        player = Instantiate(characterToSwap, playerTransform.position, playerTransform.rotation);
        GameManager.Instance.player = player;
        GameManager.Instance.gameObject.GetComponent<MenusManager>().GetPlayerInput();
        mainCamera.GetUniversalAdditionalCameraData().volumeTrigger = player.transform;
        CinamachineManager.instance.SwapCharacter();
        shopManager.GetPlayer();
    }

    public void HandleDistanceInteraction()
    {
        SetCharacterIndex();
        SwapCharacter();
    }

    public void HandleDisplayInteractKey(bool value)
    {
        interactCanvas.SetActive(value);
    }

}
