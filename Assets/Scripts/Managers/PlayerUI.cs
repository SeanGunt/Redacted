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
    public Image healthBar;
    [HideInInspector] public Color imageCooldownColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    [HideInInspector] public Color imageStartColor;

    private void Awake()
    {
        imageStartColor = qImage.color;
    }
}
