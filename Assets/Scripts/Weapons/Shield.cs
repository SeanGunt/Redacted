using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform rotatePoint;
    [SerializeField] private Sword sword;
    private bool isRotating;
    private PlayerBase playerBase;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSprite;
    private BoxCollider2D bc;
    private Animator animator;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSprite = playerBase.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        bc.enabled = false;
        trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (playerSprite.flipX == true && !isRotating)
        {
            rotatePoint.localPosition = new Vector3(-0.3f, 0.3f, 0f);
            spriteRenderer.flipX = true;
        }
        else if (playerSprite.flipX == false || isRotating)
        {
            rotatePoint.localPosition = new Vector3(0.3f, 0.3f, 0f);
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(sword.ApplyRDamage());
        }
    }

    public void HandleShieldAnims(string animName, bool isTrue)
    {
        animator.SetBool(animName, isTrue);
        isRotating = isTrue;
        bc.enabled = isTrue;
        trailRenderer.emitting = isTrue;
    }
}
