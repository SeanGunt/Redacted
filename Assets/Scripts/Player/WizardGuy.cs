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
            StartCoroutine(HandleQCooldown());
        }
    }

    protected override void HandleWAbility() 
    {

    }

    protected override void HandleEAbility() 
    {

    }

    protected override void HandleRAbility() 
    {

    }

    private void SendBolt()
    {
        Vector3 playerPos = transform.position;
        GetEnemiesOnScreen();
        List<float> distances = CalculateDistances(playerPos);
        SortEnemies(distances);
        Vector3 targetPos = enemies[0].position;
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
}
