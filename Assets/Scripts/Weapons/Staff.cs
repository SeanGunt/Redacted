using System.Collections;
using UnityEngine;

public class Staff : WeaponBase
{
    [SerializeField] private Wizard wizard;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [HideInInspector] public bool inAnimation;
    private Vector3 mousePos;
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;
    public GameObject iceballPrefab;
    public GameObject blackHolePrefab;
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

    public void GetMousePosition()
    {
        mousePos = wizard.GetMousePosition();
    }

    public void SpawnFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, staffSpawnPoint.transform.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        wizard.HandleRotation(mousePos, fireball.transform);
    }

    public void LightningBolt()
    {
        GameObject lightning = Instantiate(lightningPrefab, staffSpawnPoint.transform.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
    }

    public void Iceball()
    {
        GameObject iceball = Instantiate(iceballPrefab, staffSpawnPoint.transform.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
    }

    public void BlackHole()
    {
        GameObject blackHole = Instantiate(blackHolePrefab, wizard.GetMousePosition(), Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
    }

    
    #endregion
}
