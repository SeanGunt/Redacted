using UnityEngine;

public class Shield : MonoBehaviour
{
    private BoxCollider2D weaponCollider;
    private Animator animator;
    [SerializeField] private Sword sword;
    [SerializeField] private Knight knight;

    private void Start()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(sword.ApplyDamage());
        }
    }

    public float BashLength()
    {
        return animator.runtimeAnimatorController.animationClips[1].length;

    }

    public void Bash()
    {
        animator.SetTrigger("Bash");
    }

    public void Protect(bool isProtecting)
    {
        animator.SetBool("isProtecting", isProtecting);
    }
    public void ExecuteBash()
    {
        knight.Dash();
    }

    public void EnableCollider()
    {
        sword.CantRotate();
        sword.CantMove();
        weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        sword.CanRotate();
        sword.CanMove();
        weaponCollider.enabled = false;
    }


}
