using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject[] poolHolders;
    [HideInInspector] public GameObject player;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 144;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
