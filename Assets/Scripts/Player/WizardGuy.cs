using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardGuy : PlayerBase
{
    [SerializeField] private Staff staff;
    private List<Transform> enemies = new List<Transform>();
    protected override void HandleQAbility() 
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.Q;
            staff.HandleStaffThrustAnim("Thrust");
            SendBolt();
            SendLightning();
            StartCoroutine(HandleQCooldown());
        }
    }

    protected override void HandleWAbility() 
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.W;
            
            // @TODO
            // instead of an animation here, the staff wont move but electricity will
            // flow out of it in a straight line.

            // get the normalized direction of where the player is looking

            // 

            StartCoroutine(HandleWCooldown());
        }
    }

    protected override void HandleEAbility() 
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.E;

            // @TODO
            // again, no animation here, just a radiating pulse of electricity that
            // should flow from the tip of the staff to the edges of the maps

            StartCoroutine(HandleECooldown());
        }
    }

    protected override void HandleRAbility() 
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.R;

            // @TODO
            // this will probably just take some transformations and
            // particle systems to create. I want electricity to surround the player,
            // and for the player to grow in size and gain haste. 

            StartCoroutine(HandleECooldown());
        }
    }

    private void SendBolt()
    {
        Vector3 playerPos = transform.position;
        GetEnemiesOnScreen();
        List<float> distances = CalculateDistances(playerPos);
        SortEnemies(distances);
        IDamagable damagable = enemies[0].gameObject.GetComponent<IDamagable>();
        damagable.TakeDamage(staff.ApplyDamage());
    }

    private void SendLightning()
    {
        ParticleSystem lightningParticles = Instantiate(staff.ps, staff.transform.position + new Vector3(0f, 2.2f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 90f)), staff.gameObject.transform);

        lightningParticles.Play();
    }

    private void GetEnemiesOnScreen()
    {
        enemies.Clear();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            enemies.Add(enemyObject.transform);
        }
    }

    private List<float> CalculateDistances(Vector3 playerPosition)
    {
        List<float> distances = new List<float>();
        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(playerPosition, enemy.position);
            distances.Add(distance);
        }
        return distances;
    }

    private void SortEnemies(List<float> distances)
    {
        for (int i = 0; i <enemies.Count - 1; i++)
        {
            for (int j = i + 1; j <enemies.Count; j++)
            {
                int index1 = enemies.IndexOf(enemies[i]);
                int index2 = enemies.IndexOf(enemies[j]);

                if (distances[index1] > distances[index2])
                {
                    Transform temp = enemies[i];
                    enemies[i] = enemies[j];
                    enemies[j] = temp;
                }
            }
        }
    }
}
