using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovePointReticle movePointReticle;
    private SwordTestController swordTestController;
    [SerializeField] private float speed;
    private Vector3 positionToMove;
    private State state;
    private enum State
    {
        idle, moving
    }

    private void Awake()
    {
        movePointReticle = this.GetComponent<MovePointReticle>();
        swordTestController = this.GetComponentInChildren<SwordTestController>();
        state = State.idle;
    }
    private void Update()
    {
        HandleRightClick();
        HandleQAbility();
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

    private void HandleQAbility()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !swordTestController.inAnimation)
        {
            swordTestController.HandleSwordSwingAnim("Swing");
            Vector3 mousePos = GetMousePosition();
            HandleRotation(mousePos);
        }
    }

    private void HandleRightClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            positionToMove = GetMousePosition();
            HandleRotation(positionToMove);
            movePointReticle.CreateReticle(positionToMove);
            state = State.moving;
        }
    }
 
    private void HandleRotation(Vector3 pos)
    {
        Vector3 direction = (pos - this.transform.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }

    private void HandleMoving()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, positionToMove, speed * Time.deltaTime);
    }
    Vector3 GetMousePosition()
    {
       Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       mousePos.z = 0;
       return mousePos;
    }
}
