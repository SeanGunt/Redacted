using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHead : ItemBase
{
    [Header("Item Specific Data")]
    [SerializeField] private GameObject laserGO;
    public override void ActiveAbility()
    {
        if (activeCooldown <= 0)
        {
            StartCoroutine(Laser());
        }
    }

    public override void PassiveAbility()
    {
        base.PassiveAbility();
    }

    private IEnumerator Laser()
    {
        StartCoroutine(HandleItemCooldown());
        GameObject laser =  Instantiate(laserGO, player.transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
        RobotLaser robotLaser = laser.GetComponentInChildren<RobotLaser>();
        laser.transform.localScale = new Vector3(0.25f, 0f, 1f);
        Utilities.instance.HandleRotation(playerBase.GetMousePosition(), laser.transform);
        while (laser.transform.localScale.y <= 8f)
        {
            laser.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.4f, player.transform.position.z);
            laser.transform.localScale += new Vector3(0f, Time.deltaTime * 12.5f, 0f);
            yield return null;
        }
        robotLaser.fadeStarted = true;
    }
}
