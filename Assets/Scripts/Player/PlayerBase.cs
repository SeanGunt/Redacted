using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class PlayerBase : MonoBehaviour
{
    [Header("Classes")]
    private PlayerInput playerInput;
    private MovePointReticle movePointReticle;
    private ExperienceManager experienceManager;
    [HideInInspector] public AudioSource audioSource;
    protected WeaponBase weaponBase;
    private PlayerUI playerUI;

    [Header("Stats")]
    [SerializeField] public float baseSpeed;
    [HideInInspector] public float speed;
    [SerializeField] public float baseHealth;
    [SerializeField] public float healthRegen;
    [SerializeField] public float qCooldownAmount, wCooldownAmount, eCooldownAmount, rCooldownAmount;
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
    [SerializeField] [Range(0.0f, 1.0f)] public float damageReduction;
    protected float qCooldown = 0f, wCooldown = 0f, eCooldown = 0f, rCooldown = 0f;
    protected float qBaseCooldown, wBaseCooldown, eBaseCooldown, rBaseCooldown;
    
    [Header("Item Logic")]
    [HideInInspector] public int attacksUsed;

    [Header("Other")]
    private int raycastLayerMask = 1 << 11;
    protected State state;
    private Vector3 positionToMove;
    protected Rigidbody2D rb;
    protected Animator animator;
    private SpriteRenderer spriteRenderer;
    protected bool canUseAbility = true;
    [HideInInspector] public bool canFlipSprite = true;
    protected bool canMove = true;
    protected bool isInvincible;
    protected Collider2D cachedCollider;
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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        health = baseHealth;
        speed = baseSpeed;
        state = State.idle;
        qBaseCooldown = qCooldownAmount;
        wBaseCooldown = wCooldownAmount;
        eBaseCooldown = eCooldownAmount;
        rBaseCooldown = rCooldownAmount;
        HandleMetaProgressionStats();
    }
    virtual protected void Update()
    {
        HandleHealth();
        HandleStatsUI();
        RightClick();
        HandleAbilities();
        HandleDistanceInteract();
        HandleSpriteFlipping();
        switch(state)
        {
            case State.idle:
                HandleIdle();
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

    private void HandleDistanceInteract()
    {
        Collider2D overlappedCollider = Physics2D.OverlapCircle(transform.position, 1.25f, raycastLayerMask);
        if (overlappedCollider != null)
        {
            cachedCollider = overlappedCollider;
            Debug.Log("Overlapping Collider");
            IDistanceInteractable other = overlappedCollider.transform.gameObject.GetComponent<IDistanceInteractable>();
            other.HandleDisplayInteractKey(true);
            if (playerInput.actions["Interact"].triggered)
            {
                if (other != null)
                {
                    Debug.Log(overlappedCollider.name);
                    other.HandleDistanceInteraction();
                }
            }
        }
        else if (overlappedCollider == null)
        {
            if (cachedCollider != null)
            {
                Debug.Log("Hello");
                IDistanceInteractable other = cachedCollider.transform.gameObject.GetComponent<IDistanceInteractable>();
                other.HandleDisplayInteractKey(false);
                cachedCollider = null;
            }
        }
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
        if (playerInput.actions["RightClick"].triggered && Time.timeScale > 0f && canMove)
        {
            positionToMove = GetMousePosition();
            movePointReticle.CreateReticle(positionToMove);
            state = State.moving;
        }
    }

    private void HandleSpriteFlipping()
    {
        if (!canFlipSprite) return;
        if (GetMousePosition().x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void HandleIdle()
    {
        animator.SetBool("isWalking", false);
    }

    public Vector3 GetMousePosition()
    {
       Vector3 mousePos = playerInput.actions["PointerPosition"].ReadValue<Vector2>();
       mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       mousePos.z = 0;
       return mousePos;
    }
 
    public void HandleRotation(Vector3 pos, Transform thingToRotate)
    {
        Vector3 direction = (pos - thingToRotate.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        thingToRotate.eulerAngles = new Vector3(0,0,angle);
    }

    private void HandleMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionToMove, speed * Time.deltaTime);
        animator.SetBool("isWalking", true);
        float distanceToWalkPosition = Vector2.Distance(transform.position, positionToMove);
        if (distanceToWalkPosition < 0.01f)
        {
            state = State.idle;
        }
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

    public void TakeDamage(float damageToTake, float resistanceType)
    {
        if (isInvincible) return;
        float initialResistance = damageToTake - (damageToTake * resistanceType);
        health -= initialResistance - (initialResistance * damageReduction);
    }

    private void HandleStatsUI()
    {
        playerUI.physicalDamageText.text =  physicalDamage.ToString("0.##");
        playerUI.magicalDamageText.text = magicalDamage.ToString("0.##");
        playerUI.physicalResistanceText.text = physicalResistance.ToString("0.##");
        playerUI.magicalResistanceText.text = magicalResistance.ToString("0.##");
        playerUI.criticalChanceText.text = critChance.ToString("0.##") + "%";
        playerUI.cooldownReductionText.text = cooldownReduction.ToString("0.##") + "%";
        playerUI.speedText.text = speed.ToString("0.##");
        playerUI.pickupRangeText.text = pickupRange.ToString("0.##");
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

    private void HandleMetaProgressionStats()
    {
        physicalDamage += SaveManager.instance._gameData.extraPhysicalDamage;
        magicalDamage += SaveManager.instance._gameData.extraMagicDamage;
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
        qCooldown = qCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.qImage.color = playerUI.imageCooldownColor;
        playerUI.qImage.fillAmount = 0f;
        while(qCooldown >= 0)
        {
            qCooldown -= Time.deltaTime;
            playerUI.qImage.fillAmount += Time.deltaTime / qCooldownAmount;
            yield return null;
        }
        playerUI.qImage.fillAmount = 1f;
        playerUI.qImage.color = playerUI.imageStartColor;
    }

    protected virtual IEnumerator HandleWCooldown(float delay)
    {
        playerUI.wImage.color = Color.yellow;
        wCooldown = wCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.wImage.color = playerUI.imageCooldownColor;
        playerUI.wImage.fillAmount = 0f;
        while(wCooldown >= 0)
        {
            wCooldown -= Time.deltaTime;
            playerUI.wImage.fillAmount += Time.deltaTime / wCooldownAmount;
            yield return null;
        }
        playerUI.wImage.fillAmount = 1f;
        playerUI.wImage.color = playerUI.imageStartColor;
    }

    protected virtual IEnumerator HandleECooldown(float delay)
    {
        playerUI.eImage.color = Color.yellow;
        eCooldown = eCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.eImage.color = playerUI.imageCooldownColor;
        playerUI.eImage.fillAmount = 0f;
        while(eCooldown >= 0)
        {
            eCooldown -= Time.deltaTime;
            playerUI.eImage.fillAmount += Time.deltaTime / eCooldownAmount;
            yield return null;
        }
        playerUI.eImage.fillAmount = 1f;
        playerUI.eImage.color = playerUI.imageStartColor;
    }

    protected virtual IEnumerator HandleRCooldown(float delay)
    {
        playerUI.rImage.color = Color.yellow;
        rCooldown = rCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.rImage.color = playerUI.imageCooldownColor;
        playerUI.rImage.fillAmount = 0f;
        while(rCooldown >= 0)
        {
            rCooldown -= Time.deltaTime;
            playerUI.rImage.fillAmount += Time.deltaTime / rCooldownAmount;
            yield return null;
        }
        playerUI.rImage.fillAmount = 1f;
        playerUI.rImage.color = playerUI.imageStartColor;
    }
}
