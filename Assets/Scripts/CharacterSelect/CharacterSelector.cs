using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour, IRay
{
    [SerializeField] private int index;
    [SerializeField] private GameObject characterToSwap;
    public void SetCharacterIndex()
    {
        PlayerPrefs.SetInt("characterIndex", index);
        PlayerPrefs.Save();
        Debug.Log("Set Index");
    }

    public void SwapCharacter()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.transform;
        Destroy(player);
        Instantiate(characterToSwap, playerTransform.position, playerTransform.rotation);
    }

    public void HandleRaycastInteraction()
    {
        SetCharacterIndex();
        SwapCharacter();
    }

}
