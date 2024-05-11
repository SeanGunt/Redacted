using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerBase playerBase;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSprite = playerBase.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerSprite.flipX == true)
        {
            transform.localPosition = new Vector3(-0.3f, transform.localPosition.y, 0f);
            spriteRenderer.flipX = true;
        }
        else
        {
            transform.localPosition = new Vector3(0.3f, transform.localPosition.y, 0f);
            spriteRenderer.flipX = false;
        }
    }
}
