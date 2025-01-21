using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    public AudioMixer mixer;
    private void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        masterVolumeSlider.value = SaveManager.instance._settingsData.masterVolume;
        mixer.SetFloat("MasterVolume", Mathf.Log10(SaveManager.instance._settingsData.masterVolume) * 20f);

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        musicVolumeSlider.value = SaveManager.instance._settingsData.musicVolume;
        mixer.SetFloat("MusicVolume", Mathf.Log10(SaveManager.instance._settingsData.musicVolume) * 20f);

        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        sfxVolumeSlider.value = SaveManager.instance._settingsData.sfxVolume;
        mixer.SetFloat("SFXVolume", Mathf.Log10(SaveManager.instance._settingsData.sfxVolume) * 20f);
    }
    private void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Clamp(Mathf.Log10(value) * 20f, -60f, 0f));
        SaveManager.instance._settingsData.masterVolume = masterVolumeSlider.value;
    }
    private void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Clamp(Mathf.Log10(value) * 20f, -60f, 0f));
        SaveManager.instance._settingsData.musicVolume = musicVolumeSlider.value;
    }

    private void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Clamp(Mathf.Log10(value) * 20f, -60f, 0f));
        SaveManager.instance._settingsData.sfxVolume = sfxVolumeSlider.value;
    }
}
