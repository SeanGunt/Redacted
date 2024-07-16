using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInvisible : MonoBehaviour
{
    public ParticleSystem particles;
    private void OnBecameInvisible()
    {
        if (!gameObject.activeInHierarchy) return;
        if (particles != null)
        {
            particles.transform.parent = GameManager.Instance.poolHolders[4].transform;
            particles.Stop();
        }
        Destroy(gameObject);
    }
}
