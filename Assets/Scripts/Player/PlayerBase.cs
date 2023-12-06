using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    [Header("Classes")]
    private PlayerInput playerInput;
    private MovePointReticle movePointReticle;
    private ExperienceManager experienceManager;
    protected WeaponBase weaponBase;
    private PlayerUI playerUI;

    [Header("Stats")]
    [SerializeField] public float speed;
    [SerializeField] public float baseHealth;
    [SerializeField] public float healthRegen;
    [SerializeField] protected float qCooldownAmount, wCooldownAmount, eCooldownAmount, rCooldownAmount;
    [SerializeField] public float pickupRange;
    [HideInInspector] public float health;
    [SerializeField] public float physicalDamage;
    [SerializeField] public float magicalDamage;
    [SerializeField] public float physicalResistance;
    [SerializeField] public float magicalResistance;
    [SerializeField] public float healthPerLevel;
    [SerializeField] public float physPerLevel;
    [SerializeField] public float magPerLevel;
    [SerializeField] public float physResPerLevel;
    [SerializeField] public float magResPerLevel;
    [SerializeField] [Range(0.0f, 100.0f)] public float lifeSteal;
    [SerializeField] [Range(0.0f, 100.0f)] public float critChance;
    [SerializeField] [Range(0.0f, 100.0f)] public float cooldownReduction;
    protected float qCooldown = 0f, wCooldown = 0f, eCooldown = 0f, rCooldown = 0f;
    protected float qBaseCooldown, wBaseCooldown, eBaseCooldown, rBaseCooldown;

    [Header("Item Logic")]
    [HideInInspector] public int attacksUsed;

    [Header("Other")]
    protected State state;
    private Vector3 positionToMove;
    protected Rigidbody2D rb;
    protected bool canUseAbility = true;
    protected enum State
    {
        idle, moving
    }

    private void Awake()
    {
        movePointReticle = GetComponent<MovePointReticle>();
        weaponBase = GetComponentInChildren<WeaponBase>();
        playerInput = GetComponent<PlayerInput>();
        playerUI = GetComponent<PlayerUI>();
        experienceManager = GetComponent<ExperienceManager>();
        rb = GetComponent<Rigidbody2D>();
        health = baseHealth;
        state = State.idle;
        qBaseCooldown = qCooldownAmount;
        wBaseCooldown = wCooldownAmount;
        eBaseCooldown = eCooldownAmount;
        rBaseCooldown = rCooldownAmount;
    }
    private void Update()
    {
        HandleHealth();
        HandleStatsUI();
        RightClick();
        HandleAbilities();
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

    private void HandleAbilities()
    {
        if (playerInput.actions["LevelAbility"].inProgress) return;
        HandleQAbility();
        HandleWAbility();
        HandleEAbility();
        HandleRAbility();
    }

    protected bool CanUseAbility(string attackName, float cooldown, float abilityLevel)
    {
        if (playerInput.actions[attackName].triggered && cooldown <= 0 && abilityLevel > 0)
        {
            canUseAbility = true;
        }
        else
        {
            canUseAbility = false;
        }
        return canUseAbility;
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

    public Vector3 GetMousePosition()
    {
       Vector3 mousePos = playerInput.actions["PointerPosition"].ReadValue<Vector2>();
       mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       mousePos.z = 0;
       return mousePos;
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
        transform.position = Vector3.MoveTowards(transform.position, positionToMove, speed * Time.deltaTime);
    }

    private void HandleHealth()
    {
        playerUI.curHealthNumText.text = health.ToString("n0");
        float ratio = 1 / baseHealth;
        playerUI.healthBar.fillAmount = health * ratio;
        if (health < baseHealth)
        {
            health += healthRegen * Time.deltaTime;
        }
    }

    private void HandleStatsUI()
    {
        playerUI.physicalDamageText.text =  physicalDamage.ToString();
        playerUI.magicalDamageText.text = magicalDamage.ToString();
        playerUI.physicalResistanceText.text = physicalResistance.ToString();
        playerUI.magicalResistanceText.text = magicalResistance.ToString();
        playerUI.criticalChanceText.text = critChance.ToString() + "%";
        playerUI.cooldownReductionText.text = cooldownReduction.ToString() + "%";
    }

    public void HandleOnlevel()
    {
        baseHealth += healthPerLevel;
        health += healthPerLevel;
        physicalDamage += physPerLevel;
        magicalDamage += magPerLevel;
        physicalResistance += physResPerLevel;
        magicalResistance += magResPerLevel;
    }

    private void HandleCoolDownReduction()
    {
        qCooldownAmount = qBaseCooldown - (qBaseCooldown * (cooldownReduction / 100));
        wCooldownAmount = wBaseCooldown - (wBaseCooldown * (cooldownReduction / 100));
        eCooldownAmount = eBaseCooldown - (eBaseCooldown * (cooldownReduction / 100));
        rCooldownAmount = rBaseCooldown - (rBaseCooldown * (cooldownReduction / 100));
    }

    protected virtual IEnumerator HandleQCooldown(float delay)
    {
        playerUI.qImage.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        attacksUsed += 1;
        qCooldown = qCooldownAmount;
        playerUI.qImage.color = playerUI.imageCooldownColor;
        playerUI.qImage.fillAmount = 0f;
        while(qCooldown >= 0)
        {
            qCooldown -= Time.deltaTime;
            playerUI.qImage.fillAmount += Time.deltaTime / qCooldownAmount;
            yield return null;
        }
        playerUI.qImage.color = playerUI.imageStartColor;
    }

    protected virtual IEnumerator HandleWCooldown(float delay)
    {
        playerUI.wImage.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        attacksUsed += 1;
        wCooldown = wCooldownAmount;
        playerUI.wImage.color = playerUI.imageCooldownColor;
        playerUI.wImage.fillAmount = 0f;
        while(wCooldown >= 0)
        {
            wCooldown -= Time.deltaTime;
            playerUI.wImage.fillAmount += Time.deltaTime / wCooldownAmount;
            yield return null;
        }
        playerUI.wImage.color = playerUI.imageStartColor;
    }

    protected virtual IEnumerator HandleECooldown(float delay)
    {
        playerUI.eImage.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        attacksUsed += 1;
        eCooldown = eCooldownAmount;
        playerUI.eImage.color = playerUI.imageCooldownColor;
        playerUI.eImage.fillAmount = 0f;
        while(eCooldown >= 0)
        {
            eCooldown -= Time.deltaTime;
            playerUI.eImage.fillAmount += Time.deltaTime / eCooldownAmount;
            yield return null;
        }
        playerUI.eImage.color = playerUI.imageStartColor;
    }

    protected virtual IEnumerator HandleRCooldown(float delay)
    {
        playerUI.rImage.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        attacksUsed += 1;
        rCooldown = rCooldownAmount;
        playerUI.rImage.color = playerUI.imageCooldownColor;
        playerUI.rImage.fillAmount = 0f;
        while(rCooldown >= 0)
        {
            rCooldown -= Time.deltaTime;
            playerUI.rImage.fillAmount += Time.deltaTime / rCooldownAmount;
            yield return null;
        }
        playerUI.rImage.color = playerUI.imageStartColor;
    }
}
