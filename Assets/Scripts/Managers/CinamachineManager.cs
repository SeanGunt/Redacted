using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinamachineManager : MonoBehaviour
{
    public static CinamachineManager instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineConfiner2D confiner2D;
    private void Start()
    {
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner2D = GetComponent<CinemachineConfiner2D>();
        cinemachineVirtualCamera.LookAt = GameManager.Instance.player.transform;
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;
        confiner2D.InvalidateCache();

    }

    public void SwapCharacter()
    {
        cinemachineVirtualCamera.LookAt = GameManager.Instance.player.transform;
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;
    }
}
