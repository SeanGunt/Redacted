using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuy : PlayerBase
{
    private List<Transform> enemies = new List<Transform>();
    [SerializeField] private Sword sword;

    protected override void HandleQAbility()
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel) && !sword.inAnimation)
        {
            sword.abilityType = WeaponBase.AbilityType.Q;
            sword.HandleSwordSwingAnim("Swing");
            StartCoroutine(HandleQCooldown());
        }
    }

    protected override void HandleWAbility()
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel) && !sword.inAnimation)
        {
            sword.abilityType = WeaponBase.AbilityType.W;
            sword.HandleSwordSwingAnim("Spin");
            StartCoroutine(HandleWCooldown());
        }
    }

    protected override void HandleEAbility()
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel) && sword.inAnimation)
        {
            state = State.idle;
            sword.abilityType = WeaponBase.AbilityType.E;
            Vector3 posToDash = GetMousePosition();
            Vector3 direction = (posToDash - transform.position).normalized;
            HandleRotation(posToDash, transform);
            rb.AddForce(direction * 25, ForceMode2D.Impulse);
            sword.HandleSwordSwingAnim("Dash");
            StartCoroutine(HandleECooldown());
        }
    }

    protected override void HandleRAbility()
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel) && sword.inAnimation)
        {
            sword.abilityType = WeaponBase.AbilityType.R;
            Vector3 playerPosition = transform.position;
            GetEnemiesOnScreen();
            List<float> distances = CalculateDistances(playerPosition);
            SortEnemies(distances);
            int numEnemiesToTarget = Mathf.Min(enemies.Count, 10);
            List<Transform> enemiesToTarget = enemies.GetRange(0, numEnemiesToTarget);
            StartCoroutine(SendSwordToEnemies(enemiesToTarget));
        }
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

    private IEnumerator SendSwordToEnemies(List<Transform> enemiesToTarget)
    {
        Transform[] arrayOfEnemies = enemiesToTarget.ToArray();
        if (arrayOfEnemies.Length == 0)
        {
            yield break;
        }
        sword.GetComponent<Animator>().enabled = false;
        sword.inAnimation = true;
        sword.EnableWeaponCollider();
        sword.transform.SetParent(null, true);
        float moveSpeed = 25f;

        for (int i = 0; i < arrayOfEnemies.Length; i++)
        {
            Transform currentEnemy = arrayOfEnemies[i];

            if (currentEnemy != null)
            {
                bool reachedTarget = false;

                while (!reachedTarget && currentEnemy != null)
                {
                    float distance = Vector3.Distance(sword.transform.position, currentEnemy.position);
                    if (distance <= 0.1f)
                    {
                    reachedTarget = true;
                    }
                    else
                    {
                        sword.transform.position = Vector3.MoveTowards(sword.transform.position, currentEnemy.position, moveSpeed * Time.deltaTime);
                        HandleRotation(currentEnemy.transform.position, sword.transform);
                    }

                    yield return null;
                }
            }
        }
        sword.transform.SetParent(transform, false);
        sword.inAnimation = false;
        sword.DisableWeaponCollider();
        sword.GetComponent<Animator>().enabled = true;
        StartCoroutine(HandleRCooldown());
    }
}
