using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordGuy : PlayerBase
{
    private List<Transform> enemies = new List<Transform>();
    protected override void HandleQAbility(InputAction.CallbackContext obj)
    {
        if (!swordTestController.inAnimation && qCooldown <= 0)
        {
            swordTestController.HandleSwordSwingAnim("Swing");
            StartCoroutine(HandleQCooldown());
        }
    }

    protected override void HandleWAbility(InputAction.CallbackContext obj)
    {
        if (!swordTestController.inAnimation && wCooldown <= 0)
        {
            swordTestController.HandleSwordSwingAnim("Spin");
            StartCoroutine(HandleWCooldown());
        }
    }

    protected override void HandleEAbility(InputAction.CallbackContext obj)
    {
        if (!swordTestController.inAnimation && eCooldown <= 0)
        {
            state = State.idle;
            Vector3 posToDash = GetMousePosition();
            Vector3 direction = (posToDash - this.transform.position).normalized;
            HandleRotation(posToDash, this.transform);
            rb.AddForce(direction * 25, ForceMode2D.Impulse);
            swordTestController.HandleSwordSwingAnim("Dash");
            StartCoroutine(HandleECooldown());
        }
    }

    protected override void HandleRAbility(InputAction.CallbackContext obj)
    {
        if (!swordTestController.inAnimation && rCooldown <= 0)
        {
            Vector3 playerPosition = this.transform.position;
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
        swordTestController.GetComponent<Animator>().enabled = false;
        swordTestController.inAnimation = true;
        swordTestController.EnableWeaponCollider();
        swordTestController.transform.SetParent(null, true);
        float moveSpeed = 25f;

        for (int i = 0; i < arrayOfEnemies.Length; i++)
        {
            Transform currentEnemy = arrayOfEnemies[i];

            if (currentEnemy != null)
            {
                bool reachedTarget = false;

                while (!reachedTarget && currentEnemy != null)
                {
                    float distance = Vector3.Distance(swordTestController.transform.position, currentEnemy.position);
                    if (distance <= 0.1f)
                    {
                    reachedTarget = true;
                    }
                    else
                    {
                        swordTestController.transform.position = Vector3.MoveTowards(swordTestController.transform.position, currentEnemy.position, moveSpeed * Time.deltaTime);
                        HandleRotation(currentEnemy.transform.position, swordTestController.transform);
                    }

                    yield return null;
                }
            }
        }
        swordTestController.transform.SetParent(this.transform, false);
        swordTestController.inAnimation = false;
        swordTestController.DisableWeaponCollider();
        swordTestController.GetComponent<Animator>().enabled = true;
        StartCoroutine(HandleRCooldown());
    }
}
