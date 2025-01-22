using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : EnemyMaster
{
    private Vector3 initialPosition;
    private bool resettingPostion;
    protected override void Awake()
    {
        health = maxHealth;
        enemyID = nextID++;

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }

    protected override void Start()
    {
        initialPosition = transform.position;
    }

    protected override void Update()
    {
        if (transform.position != initialPosition && !resettingPostion)
        {
            StartCoroutine(ResetPosition());
        }
    }

    private IEnumerator ResetPosition()
    {
        resettingPostion = true;
        yield return new WaitForSeconds(6f);
        transform.position = initialPosition;
        resettingPostion = false;
    }
}
