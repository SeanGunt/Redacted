using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovePointReticle movePointReticle;
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
        state = State.idle;
    }
    private void Update()
    {
        HandleRightClick();
        switch(state)
        {
            case State.idle:
            break;
            case State.moving:
                HandleMoving();
            break;
        }
    }

    private void HandleRightClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            positionToMove = GetMousePosition();
            movePointReticle.CreateReticle(positionToMove);
            state = State.moving;
        }
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
