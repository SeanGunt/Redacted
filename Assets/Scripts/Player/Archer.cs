using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Archer : PlayerBase
{
    [SerializeField] private Bow bow;
    [SerializeField] private AudioClip dashAudioClip;

    protected override void HandleQAbility()
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            bow.abilityType = WeaponBase.AbilityType.Q;
            bow.HandleBowAnims("Fire");
            StartCoroutine(HandleQCooldown(bow.clips[1].length));
            StartCoroutine(ReturnToNormalState(bow.clips[1].length));
        }
    }

    protected override void HandleWAbility()
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel))
        {
            bow.abilityType = WeaponBase.AbilityType.W;
            bow.HandleBowAnims("RapidFire");
            StartCoroutine(HandleWCooldown(bow.clips[2].length));
            StartCoroutine(ReturnToNormalState(bow.clips[2].length));
        }
    }

    protected override void HandleEAbility()
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel))
        {
            bow.abilityType = WeaponBase.AbilityType.E;
            bow.SpawnBomb();
            StartCoroutine(HandleDashAbility());
            StartCoroutine(HandleECooldown(1f));
        }
    }

    protected override void HandleRAbility()
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel))
        {
            bow.abilityType = WeaponBase.AbilityType.R;
            bow.HandleBowAnims("ArrowRain");
            StartCoroutine(HandleRCooldown(bow.clips[3].length));
            StartCoroutine(ReturnToNormalState(bow.clips[3].length));
        }
    }

    public void Dash()
    {
        Vector3 posToDash = GetMousePosition();
        Vector3 direction = (posToDash - transform.position).normalized;
        rb.AddForce(direction * 18, ForceMode2D.Impulse);
    }

    private IEnumerator HandleDashAbility()
    {
        Dash();
        audioSource.PlayOneShot(dashAudioClip);
        canFlipSprite = false;
        canUseAbility = false;
        weaponBase.canRotate = false;
        canMove = false;
        state = State.idle;
        yield return new WaitForSeconds(0.3f);
        if (weaponBase.replicatorPurchased)
        {
            bow.SpawnBomb();
        }
        canFlipSprite = true;
        canUseAbility = true;
        weaponBase.canRotate = true;
        canMove = true;
    }

    private IEnumerator ReturnToNormalState(float lengthOfClip)
    {
        yield return new WaitForSeconds(lengthOfClip);
        bow.NotInArrowAttack();
        bow.canRotate = true;
        bow.CanFlipSprite();
    }
}
 