using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerBase : MonoBehaviour
{
    #region Classes
    public PlayerInput playerInput;
    private MovePointReticle movePointReticle;
    protected WeaponBase weaponBase;
    #endregion

    #region UI
    [SerializeField] protected Image qImage, wImage, eImage, rImage;
    public Image healthBar;
    private Color imageCooldownColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private Color imageStartColor;
    [SerializeField] private TextMeshProUGUI curHealthNum;
    #endregion

    #region Stats
    [SerializeField] public float speed, baseHealth;
    [HideInInspector] public float health;
    [SerializeField] protected float qCooldownAmount, wCooldownAmount, eCooldownAmount, rCooldownAmount;
    [SerializeField] protected float healthRegen;
    protected float qCooldown = 0f, wCooldown = 0f, eCooldown = 0f, rCooldown = 0f;
    #endregion

    #region Other
    protected State state;
    private Vector3 positionToMove;
    protected Rigidbody2D rb;
    protected enum State
    {
        idle, moving
    }
    #endregion

    private void Awake()
    {
        movePointReticle = GetComponent<MovePointReticle>();
        weaponBase = GetComponentInChildren<WeaponBase>();
        playerInput = GetComponent<PlayerInput>();
        imageStartColor = qImage.color;
        health = baseHealth;
        rb = GetComponent<Rigidbody2D>();
        state = State.idle;
    }
    private void Update()
    {
        HandleHealth();
        RightClick();
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

    private void RightClick()
    {
        if (playerInput.actions["RightClick"].triggered)
        {
            positionToMove = GetMousePosition();
            if (weaponBase.canRotate)
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
        if (!weaponBase.canMove) return;
        this.transform.position = Vector3.MoveTowards(this.transform.position, positionToMove, speed * Time.deltaTime);
    }

    private void HandleHealth()
    {
        curHealthNum.text = health.ToString("n0");
        float ratio = 1 / baseHealth;
        healthBar.fillAmount = health * ratio;
        if (health < baseHealth)
        {
            health += healthRegen * Time.deltaTime;
        }
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
       Vector3 mousePos = playerInput.actions["PointerPosition"].ReadValue<Vector2>();
       mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       mousePos.z = 0;
       return mousePos;
    }
}
