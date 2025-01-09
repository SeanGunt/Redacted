using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftBoots : ItemBase
{
    [Header("Item Specific Data")]
    [SerializeField] private float speedBuffDurationAmount;
    private float speedBuffDuration;
    [SerializeField] private float speedBuffAmount;
    private bool speedBuffActive = false;

    public override void ActiveAbility()
    {
        if (activeCooldown <= 0f)
        {
            speedBuffDuration = speedBuffDurationAmount;
            playerBase.speed += speedBuffAmount;
            speedBuffActive = true;
            StartCoroutine(HandleItemCooldown());
        }
    }

    private void DeactivateAbility()
    {
        playerBase.speed -= speedBuffAmount;
        speedBuffActive = false;
    }

    private void Update()
    {  
        if (speedBuffActive)
        {
            speedBuffDuration -= Time.deltaTime;
        }

        if (speedBuffDuration <= 0 && speedBuffActive)
        {
            DeactivateAbility();
        }
    }
}
