using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioSource musicAudioSource;
    private void Awake()
    {
        musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        musicVolumeSlider.value = SaveManager.instance._settingsData.musicVolume;
        musicAudioSource.volume = SaveManager.instance._settingsData.musicVolume;
    }

    private void OnVolumeChanged(float value)
    {
        musicAudioSource.volume = musicVolumeSlider.value;
        SaveManager.instance._settingsData.musicVolume = musicVolumeSlider.value;
    }

    public void ForceSave()
    {
        SaveManager.instance._settingsData.musicVolume = musicVolumeSlider.value;
    }
}
