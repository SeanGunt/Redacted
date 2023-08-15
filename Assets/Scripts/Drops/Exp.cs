using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropsBase
{
    [SerializeField] private float expAmount;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            gameObject.SetActive(false);
        }
    }
}
