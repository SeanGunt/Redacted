using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinamachineManager : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.LookAt = GameManager.Instance.player.transform;
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;
    }
}
