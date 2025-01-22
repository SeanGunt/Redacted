using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumDrop : DropsBase
{
    [SerializeField] private GameObject vacuumParticlesGO;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            SFXManager.instance.PlayOneShotAtPoint(transform.position, pickupAudioClip);
            Instantiate(vacuumParticlesGO, player.transform.position, Quaternion.Euler(-90f, 0f, 0f), player.transform);
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
