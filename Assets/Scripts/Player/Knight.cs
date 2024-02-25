using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PlayerBase
{
    [SerializeField] private Sword sword;

    protected override void HandleQAbility()
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel) && !sword.inAnimation)
        {
            sword.abilityType = WeaponBase.AbilityType.Q;
            HandleRotation(GetMousePosition(), transform);
            sword.HandleSwordSwingAnim("Swing");
            StartCoroutine(HandleQCooldown(sword.clips[1].length));
        }
    }

    protected override void HandleWAbility()
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel) && !sword.inAnimation)
        {
            sword.abilityType = WeaponBase.AbilityType.W;
            HandleRotation(GetMousePosition(), transform);
            sword.HandleSwordSwingAnim("Spin");
            StartCoroutine(HandleWCooldown(sword.clips[2].length));
        }
    }

    protected override void HandleEAbility()
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel) && !sword.inAnimation)
        {
            state = State.idle;
            sword.abilityType = WeaponBase.AbilityType.E;
            sword.shield.Bash();
            StartCoroutine(HandleECooldown(sword.shield.BashLength()));
        }
    }

    protected override void HandleRAbility()
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel) && !sword.inAnimation)
        {
            StartCoroutine(ProtectAndServe());
            StartCoroutine(HandleRCooldown(10f));
        }
    }

    public void Dash()
    {
        Vector3 posToDash = GetMousePosition();
        Vector3 direction = (posToDash - transform.position).normalized;
        HandleRotation(posToDash, transform);
        rb.AddForce(direction * 25, ForceMode2D.Impulse);
    }

    private IEnumerator ProtectAndServe()
    {
        sword.shield.Protect(true);
        qCooldownAmount *= 0.5f;
        wCooldownAmount *=  0.5f;
        eCooldownAmount *=  0.5f;
        damageReduction += 0.75f;
        speed /= 2;
        yield return new WaitForSeconds(10);
        sword.shield.Protect(false);
        qCooldownAmount /=  0.5f;
        wCooldownAmount /=  0.5f;
        eCooldownAmount /=  0.5f;
        damageReduction -= 0.75f;
        speed *= 2;
    }
}
