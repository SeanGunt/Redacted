using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterSelector : MonoBehaviour, IRay
{
    [SerializeField] private int index;
    [SerializeField] private GameObject characterToSwap;
    [SerializeField] private ShopManager shopManager;
    private Camera mainCamera;
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

    public void HandleRaycastInteraction()
    {
        SetCharacterIndex();
        SwapCharacter();
    }

}
