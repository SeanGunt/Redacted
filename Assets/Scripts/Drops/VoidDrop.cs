using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDrop : DropsBase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {

        }
    }
}
