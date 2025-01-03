using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject[] poolHolders;
    public Texture2D cursorSelectedTexture;
    public Texture2D cursorDefaultTexture;
    public Light2D globalLight;
    [HideInInspector] public GameObject player;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 144;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FreezeTime()
    {
        EnemyMaster[] enemies = FindObjectsOfType<EnemyMaster>();
        if (enemies == null) return;
        foreach (EnemyMaster enemy in enemies)
        {
            enemy.frozen = true;
            if (enemy.agent != null)
            {
                enemy.agent.speed = 0f;
            }
        }

        Animator weaponAnimator = player.GetComponentInChildren<WeaponBase>().animator;
        Animator playerAnimator = player.GetComponent<Animator>();
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            if (animator == weaponAnimator) continue;
            if (animator == playerAnimator) continue;
            animator.speed = 0f;
        }
    }

    public void UnFreezeTime()
    {
        EnemyMaster[] enemies = FindObjectsOfType<EnemyMaster>();
        if (enemies == null) return;
        foreach (EnemyMaster enemy in enemies)
        {
            enemy.frozen = false;
            if (enemy.agent != null)
            {
                enemy.agent.speed = enemy.baseSpeed;
            }
        }

        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            animator.speed = 1f;
        }
    }
}
