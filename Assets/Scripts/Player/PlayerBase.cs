using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour, IShopFreeze
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
    [HideInInspector] public float totalPhysicalDamage;
    [HideInInspector] public float physicalDamageMultiplier = 1.0f;
    [SerializeField] public float magicalDamage;
    [HideInInspector] public float totalMagicalDamage;
    [HideInInspector] public float magicalDamageMultiplier = 1.0f;
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
    protected float qActiveCooldownReductionMultiplier = 1f, wActiveCooldownReductionMultiplier = 1f, 
    eActiveCooldownReductionMultiplier = 1f, rActiveCooldownReductionMultiplier = 1f;
    
    [Header("Item Logic")]
    [HideInInspector] public int attacksUsed;

    [Header("Other")]
    [SerializeField] private AudioClip playerDamagedClip;
    [SerializeField] private AudioClip playerBigDamageClip;
    protected int raycastLayerMask = 1 << 11;
    protected State state;
    private Vector3 positionToMove;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Material material;
    protected SpriteRenderer spriteRenderer;
    protected bool canUseAbility = true;
    [HideInInspector] public bool canFlipSprite = true;
    [HideInInspector] public bool dead = false;
    protected bool frozenByShop;
    protected bool qCoolingDown, wCoolingDown, eCoolingDown, rCoolingDown;
    protected bool canMove = true;
    protected bool isInvincible;
    protected Collider2D cachedCollider;
    protected enum State
    {
        idle, moving, dead
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

        material = Instantiate(spriteRenderer.sharedMaterial);
        spriteRenderer.material = material;
        material.SetColor("_Color", Color.black);
    }
    virtual protected void Update()
    {
        HandleHealth();
        HandleDamageMultipliers();
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
            case State.dead:
            
            break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        state = State.idle;
    }

    private void HandleDistanceInteract()
    {
        Collider2D overlappedCollider = Physics2D.OverlapCircle(transform.position, 1f, raycastLayerMask);
        if (overlappedCollider != null)
        {
            cachedCollider = overlappedCollider;
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
        if (playerInput.actions["LevelAbility"].inProgress || frozenByShop) return;
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
        if (health < 0)
        {
            state = State.dead;
            dead = true;
            return;
        }
        playerUI.curHealthNumText.text = health.ToString("n0");
        float ratio = 1 / baseHealth;
        playerUI.healthBar.fillAmount = health * ratio;
        if (health < baseHealth)
        {
            if (!frozenByShop)
            {
                health += healthRegen * Time.deltaTime;
            }
        }
    }

    public void TakeDamage(float damageToTake, float resistanceType)
    {
        if (isInvincible) return;
        float initialResistance = damageToTake - (damageToTake * resistanceType);
        float totalDamageTaken = initialResistance - (initialResistance * damageReduction);
        health -= totalDamageTaken;
        StartCoroutine(ChangeColor(totalDamageTaken));
    }

    private IEnumerator ChangeColor(float damageTaken)
    {
        if (damageTaken >= 50f)
        {
            material.SetColor("_Color", Color.red);
            audioSource.PlayOneShot(playerBigDamageClip);
        }
        else
        {
            material.SetColor("_Color", Color.white);
            audioSource.PlayOneShot(playerDamagedClip);
        }
        yield return new WaitForSeconds(0.1f);
        material.SetColor("_Color", Color.black);
        yield break;
    }

    private void HandleStatsUI()
    {
        playerUI.physicalDamageText.text =  totalPhysicalDamage.ToString("0.##");
        playerUI.magicalDamageText.text = totalMagicalDamage.ToString("0.##");
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

    private void HandleDamageMultipliers()
    {
        totalMagicalDamage = magicalDamage * magicalDamageMultiplier;
        totalPhysicalDamage = physicalDamage * physicalDamageMultiplier;
    }

    public void HandleOnShopFreeze()
    {
        frozenByShop = true;
        playerUI.qImage.color = new Color(1f, 0f, 0f, 0.7f);
        playerUI.wImage.color = new Color(1f, 0f, 0f, 0.7f);
        playerUI.eImage.color = new Color(1f, 0f, 0f, 0.7f);
        playerUI.rImage.color = new Color(1f, 0f, 0f, 0.7f);
    }

    public void HandleOnShopUnFreeze()
    {
        frozenByShop = false;
        if (qCoolingDown)
        {
            playerUI.qImage.color = playerUI.imageCooldownColor;
        }
        else
        {
            playerUI.qImage.color = Color.white;
        }

        if (wCoolingDown)
        {
            playerUI.wImage.color = playerUI.imageCooldownColor;
        }
        else
        {
            playerUI.wImage.color = Color.white;
        }

        if (eCoolingDown)
        {
            playerUI.eImage.color = playerUI.imageCooldownColor;
        }
        else
        {
            playerUI.eImage.color = Color.white;
        }

        if (rCoolingDown)
        {
            playerUI.rImage.color = playerUI.imageCooldownColor;
        }
        else
        {
            playerUI.rImage.color = Color.white;
        }
        
    }

    private void HandleCoolDownReduction()
    {
        qCooldownAmount = qBaseCooldown - (qBaseCooldown * (cooldownReduction / 100));
        wCooldownAmount = wBaseCooldown - (wBaseCooldown * (cooldownReduction / 100));
        eCooldownAmount = eBaseCooldown - (eBaseCooldown * (cooldownReduction / 100));
        rCooldownAmount = rBaseCooldown - (rBaseCooldown * (cooldownReduction / 100));
    }

    public void HandleActiveCooldownMultipliers(float addedCooldownReduction)
    {
        if (qCoolingDown)
        {
            qActiveCooldownReductionMultiplier += addedCooldownReduction / 10f;
        }

        if (wCoolingDown)
        {
            wActiveCooldownReductionMultiplier += addedCooldownReduction / 10f;
        }

        if (eCoolingDown)
        {
            eActiveCooldownReductionMultiplier += addedCooldownReduction / 10f;
        }

        if (rCoolingDown)
        {
            rActiveCooldownReductionMultiplier += addedCooldownReduction / 10f;
        }
    }

    protected virtual IEnumerator HandleQCooldown(float delay)
    {
        qCoolingDown = true;
        playerUI.qImage.color = Color.yellow;
        qCooldown = qCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.qImage.color = playerUI.imageCooldownColor;
        playerUI.qImage.fillAmount = 0f;
        while(qCooldown >= 0)
        {
            if (!frozenByShop)
            {
                qCooldown -= Time.deltaTime * qActiveCooldownReductionMultiplier;
                playerUI.qImage.fillAmount += Time.deltaTime / qCooldownAmount;
            }
            yield return null;
        }
        qActiveCooldownReductionMultiplier = 1f;
        playerUI.qImage.fillAmount = 1f;
        playerUI.qImage.color = playerUI.imageStartColor;
        qCoolingDown = false;
    }

    protected virtual IEnumerator HandleWCooldown(float delay)
    {
        wCoolingDown = true;
        playerUI.wImage.color = Color.yellow;
        wCooldown = wCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.wImage.color = playerUI.imageCooldownColor;
        playerUI.wImage.fillAmount = 0f;
        while(wCooldown >= 0)
        {
            if (!frozenByShop)
            {
                wCooldown -= Time.deltaTime * wActiveCooldownReductionMultiplier;
                playerUI.wImage.fillAmount += Time.deltaTime / wCooldownAmount;
            }
            yield return null;
        }
        wActiveCooldownReductionMultiplier = 1f;
        playerUI.wImage.fillAmount = 1f;
        playerUI.wImage.color = playerUI.imageStartColor;
        wCoolingDown = false;
    }

    protected virtual IEnumerator HandleECooldown(float delay)
    {
        eCoolingDown = true;
        playerUI.eImage.color = Color.yellow;
        eCooldown = eCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.eImage.color = playerUI.imageCooldownColor;
        playerUI.eImage.fillAmount = 0f;
        while(eCooldown >= 0)
        {
            if (!frozenByShop)
            {
                eCooldown -= Time.deltaTime * eActiveCooldownReductionMultiplier;
                playerUI.eImage.fillAmount += Time.deltaTime / eCooldownAmount; 
            }
            yield return null;
        }
        eActiveCooldownReductionMultiplier = 1f;
        playerUI.eImage.fillAmount = 1f;
        playerUI.eImage.color = playerUI.imageStartColor;
        eCoolingDown = false;
    }

    protected virtual IEnumerator HandleRCooldown(float delay)
    {
        rCoolingDown = true;
        playerUI.rImage.color = Color.yellow;
        rCooldown = rCooldownAmount;
        yield return new WaitForSeconds(delay);
        playerUI.rImage.color = playerUI.imageCooldownColor;
        playerUI.rImage.fillAmount = 0f;
        while(rCooldown >= 0)
        {
            if (!frozenByShop)
            {
                rCooldown -= Time.deltaTime * rActiveCooldownReductionMultiplier;
                Debug.Log("R Cooldown = " + rCooldown);
                playerUI.rImage.fillAmount += Time.deltaTime / rCooldownAmount;
            }
            yield return null;
        }
        rActiveCooldownReductionMultiplier = 1f;
        playerUI.rImage.fillAmount = 1f;
        playerUI.rImage.color = playerUI.imageStartColor;
        rCoolingDown = false;
    }
}
