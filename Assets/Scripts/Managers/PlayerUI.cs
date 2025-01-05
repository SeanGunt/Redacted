using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Image qImage;
    public Image wImage;
    public Image eImage;
    public Image rImage;
    public Image expBar;
    public TextMeshProUGUI curHealthNumText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI physicalDamageText, magicalDamageText, physicalResistanceText, magicalResistanceText;
    public TextMeshProUGUI criticalChanceText, cooldownReductionText;
    public TextMeshProUGUI qLevelText, wLevelText, eLevelText, rLevelText;
    public Image healthBar;
    [HideInInspector] public Color imageCooldownColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    [HideInInspector] public Color imageStartColor;
    public GameObject pickupTimerHolder;
    public GameObject[] levelIconsGO;
    public Dictionary<string, GameObject> levelIconsDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        imageStartColor = qImage.color;
        levelIconsDict["LevelQ"] = levelIconsGO[0];
        levelIconsDict["LevelW"] = levelIconsGO[1];
        levelIconsDict["LevelE"] = levelIconsGO[2];
        levelIconsDict["LevelR"] = levelIconsGO[3];
    }
}
