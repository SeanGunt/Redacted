using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float musicVolume;
    public float sfxVolume;
    public Color reticleColor;

    public SettingsData()
    {
        musicVolume = 0.5f;
        sfxVolume = 0.5f;
        reticleColor = Color.green;
    }
}
