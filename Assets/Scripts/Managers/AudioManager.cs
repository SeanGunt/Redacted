using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioSource musicAudioSource;
    private void Awake()
    {
        musicVolumeSlider.value = SaveManager.instance._settingsData.musicVolume;
        musicAudioSource.volume = SaveManager.instance._settingsData.musicVolume;

        musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        musicAudioSource.volume = musicVolumeSlider.value;
        SaveManager.instance._settingsData.musicVolume = musicVolumeSlider.value;
    }
}
