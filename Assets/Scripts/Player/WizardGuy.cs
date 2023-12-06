using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardGuy : PlayerBase
{
    [SerializeField] private Staff staff;
    [SerializeField] private Transform particleSpawnPoint;
    private ParticleSystem lightningParticles;
    private List<Transform> enemies = new List<Transform>();
    protected override void HandleQAbility() 
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.Q;
            staff.HandleStaffThrustAnim("Thrust");
            SendBolt();
            StartCoroutine(HandleQCooldown(0f));
        }
    }

    protected override void HandleWAbility() 
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.W;
            staff.CantRotate();
            staff.HandleStaffThrustAnim("Thrust");
            SendLightning();
            StartCoroutine(DestroyLightningParticle());
            
            // @TODO
            // instead of an animation here, the staff wont move but electricity will
            // flow out of it in a straight line.

            // get the normalized direction of where the player is looking

            // 

            StartCoroutine(HandleWCooldown(0f));
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

            StartCoroutine(HandleECooldown(0f));
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

            StartCoroutine(HandleECooldown(0f));
        }
    }
    #region QAbility
    private void SendBolt()
    {
        Vector3 playerPos = transform.position;
        GetEnemiesOnScreen();
        List<float> distances = CalculateDistances(playerPos);
        SortEnemies(distances);
        IDamagable damagable = enemies[0].gameObject.GetComponent<IDamagable>();
        damagable.TakeDamage(staff.ApplyDamage());
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

    #endregion

    #region WAbility
    private void SendLightning()
    {
        lightningParticles = Instantiate(
            staff.ps, particleSpawnPoint.position, 
            Quaternion.Euler(new Vector3(0f, 0f, staff.transform.eulerAngles.z + 90f)), 
            staff.gameObject.transform
        );
        
        lightningParticles.Play();
    }

    private IEnumerator DestroyLightningParticle()
    {
        float particleDuration = 1.0f;
        while (particleDuration >= 0)
        {
            particleDuration -= Time.deltaTime;
            yield return null;
        }
        staff.CanRotate();
        Destroy(lightningParticles.gameObject);
    }
    
    #endregion
}
