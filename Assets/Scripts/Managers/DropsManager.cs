using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    public static DropsManager instance;
    [HideInInspector] public bool speedPickupActive;
    [HideInInspector] public bool vacuumPickupActive;
    [HideInInspector] public bool voidPickupActive;
    [HideInInspector] public float speedTimer;
    [HideInInspector] public float vacuumTimer;
    [HideInInspector] public float voidTimer;
    private void Awake()
    {
        instance = this;
    }
}
