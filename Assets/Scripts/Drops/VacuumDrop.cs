using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumDrop : DropsBase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            DropsBase[] allDrops = FindObjectsOfType<DropsBase>();
            if (allDrops == null) return;
            foreach(DropsBase dropsBase in allDrops)
            {
                dropsBase.StartVacuuming();
                gameObject.SetActive(false);
            }
        }
    }
}
