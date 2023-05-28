using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private MovePointReticle movePointReticle;
    protected SwordTestController swordTestController;
    protected Rigidbody2D rb;
    [SerializeField] private float speed, health;
    protected bool canRotate;
    protected bool canMove = true;
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
                HandleRotation(positionToMove);
            }
            movePointReticle.CreateReticle(positionToMove);
            state = State.moving;
        }
    }
 
    protected void HandleRotation(Vector3 pos)
    {
        Vector3 direction = (pos - this.transform.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }

    private void HandleMoving()
    {
        if (!swordTestController.canMove) return;
        this.transform.position = Vector3.MoveTowards(this.transform.position, positionToMove, speed * Time.deltaTime);
    }
    public Vector3 GetMousePosition()
    {
       Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       mousePos.z = 0;
       return mousePos;
    }
}
