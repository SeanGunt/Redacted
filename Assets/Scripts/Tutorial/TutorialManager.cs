using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    
    public static TutorialManager instance;
    public AudioSource musicSource;
    [SerializeField] private GameObject tutorialCanvasGO;
    private GameObject playerUI;
    private TextMeshProUGUI awakenText;
    private Image backgroundImage;

    private void Awake()
    {
        instance = this;
        awakenText = tutorialCanvasGO.GetComponentInChildren<TextMeshProUGUI>();
        backgroundImage = tutorialCanvasGO.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        if (!SaveManager.instance._gameData.tutorialCompleted)
        {
            musicSource.gameObject.SetActive(false);
            tutorialCanvasGO.SetActive(true);
            playerUI.SetActive(false);
            StartCoroutine(FadeText());
        }
        else
        {
            tutorialCanvasGO.SetActive(false);
        }
    }

    private IEnumerator FadeText()
    {
        yield return new WaitForSeconds(2f);
        while (awakenText.color.a > 0)
        {
            awakenText.color -= new Color(0f, 0f, 0f, Time.deltaTime / 2f);
            yield return null;
        }
        playerUI.SetActive(true);
        backgroundImage.enabled = false;
    }
}
