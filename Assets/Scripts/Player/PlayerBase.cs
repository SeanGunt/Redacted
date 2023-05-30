using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private MovePointReticle movePointReticle;
    protected SwordTestController swordTestController;
    protected Rigidbody2D rb;
    [SerializeField] private float speed, health;
    [SerializeField] protected float qCooldownAmount, wCooldownAmount, eCooldownAmount, rCooldownAmount;
    protected float qCooldown = 0f, wCooldown = 0f, eCooldown = 0f, rCooldown = 0f;
    private Vector3 positionToMove;
    protected State state;
    protected enum State
    {
        idle, moving
    }

    private void Awake()
    {
        movePointReticle = this.GetComponent<MovePointReticle>();
        swordTestController = this.GetComponentInChildren<SwordTestController>();
        rb = this.GetComponent<Rigidbody2D>();
        state = State.idle;
    }
    private void Update()
    {
        HandleRightClick();
        HandleQAbility();
        HandleWAbility();
        HandleEAbility();
        HandleRAbility();

        switch(state)
        {
            case State.idle:
            break;
            case State.moving:
                HandleMoving();
            break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        state = State.idle;
    }

    protected virtual void HandleQAbility()
    {
        
    }

    protected virtual void HandleWAbility()
    {

    }

    protected virtual void HandleEAbility()
    {

    }

    protected virtual void HandleRAbility()
    {

    }

    private void HandleRightClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            positionToMove = GetMousePosition();
            if (swordTestController.canRotate)
            {
                HandleRotation(positionToMove, this.transform);
            }
            movePointReticle.CreateReticle(positionToMove);
            state = State.moving;
        }
    }
 
    protected void HandleRotation(Vector3 pos, Transform thingToRotate)
    {
        Vector3 direction = (pos - thingToRotate.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        thingToRotate.eulerAngles = new Vector3(0,0,angle);
    }

    private void HandleMoving()
    {
        if (!swordTestController.canMove) return;
        this.transform.position = Vector3.MoveTowards(this.transform.position, positionToMove, speed * Time.deltaTime);
    }

    protected virtual IEnumerator HandleQCooldown()
    {
        qCooldown = qCooldownAmount;
        while(qCooldown >= 0)
        {
            qCooldown -= Time.deltaTime;
            yield return null;
        }
    }

    protected virtual IEnumerator HandleWCooldown()
    {
        wCooldown = wCooldownAmount;
        while(wCooldown >= 0)
        {
            wCooldown -= Time.deltaTime;
            yield return null;
        }
    }

    protected virtual IEnumerator HandleECooldown()
    {
        eCooldown = eCooldownAmount;
        while(eCooldown >= 0)
        {
            eCooldown -= Time.deltaTime;
            yield return null;
        }
    }

    protected virtual IEnumerator HandleRCooldown()
    {
        rCooldown = rCooldownAmount;
        while(rCooldown >= 0)
        {
            rCooldown -= Time.deltaTime;
            yield return null;
        }
    }

    public Vector3 GetMousePosition()
    {
       Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       mousePos.z = 0;
       return mousePos;
    }
}
