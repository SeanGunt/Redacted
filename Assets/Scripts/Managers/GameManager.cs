using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Components;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject[] poolHolders;
    public Texture2D cursorSelectedTexture;
    public Texture2D cursorDefaultTexture;
    public Light2D globalLight;
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

    public void FreezeTime()
    {
        var freezables = FindObjectsOfType<MonoBehaviour>().OfType<IFreezable>();
        foreach(var freezable in freezables)
        {
            freezable.HandleOnFreeze();
        }
    }

    public void UnFreezeTime()
    {
        var freezables = FindObjectsOfType<MonoBehaviour>().OfType<IFreezable>();
        foreach(var freezable in freezables)
        {
            freezable.HandleOnUnfreeze();
        }
    }
}
