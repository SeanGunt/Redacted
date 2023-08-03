using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] InputActionReference rightClickRef, mousePosRef, QAttackRef, WAttackRef, EAttackRef, RAttackRef;
    private Vector2 mousePos;
    private MovePointReticle movePointReticle;
    protected SwordTestController swordTestController;
    protected Rigidbody2D rb;
    [SerializeField] protected Image qImage, wImage, eImage, rImage;
    private Color imageCooldownColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private Color imageStartColor;
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
        imageStartColor = qImage.color;
        rb = this.GetComponent<Rigidbody2D>();
        state = State.idle;
    }
    private void Update()
    {
        //HandleRightClick();
        //HandleQAbility();
        //HandleWAbility();
        //HandleEAbility();
        //HandleRAbility();

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

    protected virtual void HandleQAbility(InputAction.CallbackContext obj)
    {
        
    }

    protected virtual void HandleWAbility(InputAction.CallbackContext obj)
    {

    }

    protected virtual void HandleEAbility(InputAction.CallbackContext obj)
    {

    }

    protected virtual void HandleRAbility(InputAction.CallbackContext obj)
    {

    }

    private void OnEnable()
    {
        rightClickRef.action.performed += RightClick;
        QAttackRef.action.performed += HandleQAbility;
        WAttackRef.action.performed += HandleWAbility;
        EAttackRef.action.performed += HandleEAbility;
        RAttackRef.action.performed += HandleRAbility;
    }

    private void OnDisable()
    {
        rightClickRef.action.performed -= RightClick;
        QAttackRef.action.performed -= HandleQAbility;
        WAttackRef.action.performed -= HandleWAbility;
        EAttackRef.action.performed -= HandleEAbility;
        RAttackRef.action.performed -= HandleRAbility;
    }

    private void RightClick(InputAction.CallbackContext obj)
    {
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
            positionToMove = GetMousePosition();
            if (swordTestController.canRotate)
            {
                HandleRotation(positionToMove, this.transform);
            }
            movePointReticle.CreateReticle(positionToMove);
            state = State.moving;
        //}
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
        qImage.color = imageCooldownColor;
        qImage.fillAmount = 0f;
        while(qCooldown >= 0)
        {
            qCooldown -= Time.deltaTime;
            qImage.fillAmount += Time.deltaTime / qCooldownAmount;
            yield return null;
        }
        qImage.color = imageStartColor;
    }

    protected virtual IEnumerator HandleWCooldown()
    {
        wCooldown = wCooldownAmount;
        wImage.color = imageCooldownColor;
        wImage.fillAmount = 0f;
        while(wCooldown >= 0)
        {
            wCooldown -= Time.deltaTime;
            wImage.fillAmount += Time.deltaTime / wCooldownAmount;
            yield return null;
        }
        wImage.color = imageStartColor;
    }

    protected virtual IEnumerator HandleECooldown()
    {
        eCooldown = eCooldownAmount;
        eImage.color = imageCooldownColor;
        eImage.fillAmount = 0f;
        while(eCooldown >= 0)
        {
            eCooldown -= Time.deltaTime;
            eImage.fillAmount += Time.deltaTime / eCooldownAmount;
            yield return null;
        }
        eImage.color = imageStartColor;
    }

    protected virtual IEnumerator HandleRCooldown()
    {
        rCooldown = rCooldownAmount;
        rImage.color = imageCooldownColor;
        rImage.fillAmount = 0f;
        while(rCooldown >= 0)
        {
            rCooldown -= Time.deltaTime;
            rImage.fillAmount += Time.deltaTime / rCooldownAmount;
            yield return null;
        }
        rImage.color = imageStartColor;
    }

    public Vector3 GetMousePosition()
    {
       Vector3 mousePos = mousePosRef.action.ReadValue<Vector2>();
       mousePos.z = 0;
       mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       return mousePos;
    }
}
