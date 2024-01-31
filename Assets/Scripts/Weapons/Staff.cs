using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Staff : WeaponBase
{
    [SerializeField] public ParticleSystem ps;
    [SerializeField] private WizardGuy wizardGuy;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [HideInInspector] public bool inAnimation;
    public GameObject fireballPrefab;
    public GameObject staffSpawnPoint;

    void Start()
    {   
        weaponCollider = GetComponent<BoxCollider2D>();
        weaponCollider.enabled = false;
    }

    #region Animation Events
    public void HandleStaffAnims(string animToPlay)
    {
        animator.SetTrigger(animToPlay);
    }

    public void SpawnFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, staffSpawnPoint.transform.position, wizardGuy.transform.rotation, GameManager.Instance.poolHolders[3].transform);
    }
    
    #endregion
}
