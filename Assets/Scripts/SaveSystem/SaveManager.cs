using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public GameData _gameData;
    public SettingsData _settingsData;
    string gameDataPath;
    string settingsDataPath;
    private void Awake()
    {
        instance = this;

        gameDataPath = Application.persistentDataPath + "/SaveData.json";
        settingsDataPath = Application.persistentDataPath + "/SettingsData.json";
        LoadSettingsData();
        LoadGameData();
        Debug.Log("Music volume set at " + _settingsData.musicVolume);
    }

    private void OnApplicationQuit()
    {
        SaveSettingsData(_settingsData);
        SaveGameData(_gameData);
    }

    public void SaveSettingsData(SettingsData settingsData)
    {
        string json = JsonUtility.ToJson(settingsData, true);
        File.WriteAllText(settingsDataPath, json);
    }

    public void SaveGameData(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(gameDataPath, json);
    }

    public SettingsData LoadSettingsData()
    {
        if (File.Exists(settingsDataPath))
        {
            string json = File.ReadAllText(settingsDataPath);
            _settingsData = JsonUtility.FromJson<SettingsData>(json);
            return _settingsData;
        }
        else
        {
            Debug.LogWarning("SettingsData not found, creating new data from defaults");
            _settingsData = new();
            SaveSettingsData(_settingsData);
            return _settingsData;
        }
    }

    public GameData LoadGameData()
    {
        if (File.Exists(gameDataPath))
        {
            string json = File.ReadAllText(gameDataPath);
            _gameData = JsonUtility.FromJson<GameData>(json);
            return _gameData;
        }
        else
        {
            Debug.LogWarning("GameData not found, creating new data from defaults");
            _gameData = new();
            SaveGameData(_gameData);
            return _gameData;
        }
    }
}
