using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCompletionTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private void Awake()
    {
        tutorialManager = GetComponentInParent<TutorialManager>();
    }

    private void Start()
    {
        if (SaveManager.instance._gameData.tutorialCompleted)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialManager.musicSource.gameObject.SetActive(true);
            SaveManager.instance._gameData.tutorialCompleted = true;
            SaveManager.instance.SaveGameData(SaveManager.instance._gameData);
            Destroy(gameObject);
        }
    }
}
