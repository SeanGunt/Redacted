using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfCorruption : ItemBase
{
    [Header("Item Specific Data")]
    [SerializeField] private GameObject corruptionGO;
    private GameObject corruption;
    private Rigidbody2D rb;
    private void Start()
    {
        PassiveAbility();
    }
    public override void PassiveAbility()
    {
        corruption = Instantiate(corruptionGO, new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z), Quaternion.identity, player.transform);
        rb = corruption.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
        corruption.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
    }
}
