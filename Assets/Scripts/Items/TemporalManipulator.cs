using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalManipulator : ItemBase
{
    [SerializeField] private GameObject slowAuraGO;
    private GameObject slowAura;
    private Rigidbody2D rb;
    private void Start()
    {
        PassiveAbility();
    }
    public override void ActiveAbility()
    {
        if (activeCooldown <= 0)
        {
            playerBase.gameObject.transform.position = playerBase.GetMousePosition();
            StartCoroutine(HandleItemCooldown());
        }
    }

    public override void PassiveAbility()
    {
        slowAura = Instantiate(slowAuraGO, new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z), Quaternion.identity, player.transform);
        rb = slowAura.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
        slowAura.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
    }
}
