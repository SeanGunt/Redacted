using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private RawImage reticleImage;
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;

    private void Awake()
    {
        reticleImage.color = SaveManager.instance._settingsData.reticleColor;
        redSlider.value = SaveManager.instance._settingsData.reticleColor.r;
        greenSlider.value = SaveManager.instance._settingsData.reticleColor.g;
        blueSlider.value = SaveManager.instance._settingsData.reticleColor.b;
        Debug.Log("Settings Manager Awake Called");
    }

    private void OnEnable()
    {
        redSlider.onValueChanged.AddListener(ChangeReticleColor);
        blueSlider.onValueChanged.AddListener(ChangeReticleColor);
        greenSlider.onValueChanged.AddListener(ChangeReticleColor);
        Debug.Log("Settings Manager OnEnable Called");
    }

    private void OnDisable()
    {
        SaveManager.instance._settingsData.reticleColor = new Color(redSlider.value, greenSlider.value, blueSlider.value, 1f);
        redSlider.onValueChanged.RemoveListener(ChangeReticleColor);
        blueSlider.onValueChanged.RemoveListener(ChangeReticleColor);
        greenSlider.onValueChanged.RemoveListener(ChangeReticleColor);
        Debug.Log("Settings Manager OnDisable Called");
    }


    public void ChangeReticleColor(float value)
    {
        reticleImage.color = new Color(redSlider.value, greenSlider.value, blueSlider.value, 1f);
    }
}
