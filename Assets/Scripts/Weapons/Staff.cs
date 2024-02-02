using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Staff : WeaponBase
{
    [SerializeField] private WizardGuy wizardGuy;
    [HideInInspector] public BoxCollider2D weaponCollider;
    [HideInInspector] public bool inAnimation;
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;
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

    private void LightningBolt()
    {
        GameObject lightning = Instantiate(lightningPrefab, staffSpawnPoint.transform.position, transform.rotation, GameManager.Instance.poolHolders[3].transform);
        lightning.transform.localScale = new Vector3(1f,0,1f);
        StartCoroutine(ExtendBolt(lightning.transform));
    }

    private IEnumerator ExtendBolt(Transform lightning)
    {
        while (lightning.localScale.y <= 4)
        {
            lightning.transform.localScale += new Vector3(0f, 25f * Time.deltaTime, 0f);
            yield return null;
        }
        BoxCollider2D bc = lightning.GetComponentInChildren<BoxCollider2D>();
        bc.enabled = false;
        SpriteRenderer sr = lightning.GetComponentInChildren<SpriteRenderer>();
        while (sr.color.a >= 0)
        {
            sr.color -= new Color(0f,0f,0f, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Destroy(lightning.gameObject);
        yield return null;
    }
    
    #endregion
}
