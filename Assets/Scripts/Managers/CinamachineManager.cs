using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinamachineManager : MonoBehaviour
{
    public static CinamachineManager instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start()
    {
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.LookAt = GameManager.Instance.player.transform;
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;

    }

    public void SwapCharacter()
    {
        cinemachineVirtualCamera.LookAt = GameManager.Instance.player.transform;
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;
    }
}
