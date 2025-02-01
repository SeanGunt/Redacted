using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAura : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyMaster enemyMaster = other.GetComponent<EnemyMaster>();
        if (enemyMaster != null && enemyMaster.agent != null)
        {
            enemyMaster.speed -= enemyMaster.baseSpeed * 0.25f;
            enemyMaster.animator.speed -= 0.25f;
            enemyMaster.agent.speed = enemyMaster.speed;
            Debug.Log("Hit Enemy");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyMaster enemyMaster = other.GetComponent<EnemyMaster>();
        if (enemyMaster != null && enemyMaster.agent != null)
        {
            enemyMaster.speed += enemyMaster.baseSpeed * 0.25f;
            enemyMaster.animator.speed += 0.25f;
            enemyMaster.agent.speed = enemyMaster.speed;
        }
    }
}
