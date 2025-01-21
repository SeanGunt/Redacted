using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject textToReveal;
    private void Start()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (player == null)
        {
            StartCoroutine(OnCharacterSwap());
            return;
        }
        float distanceToPlayer =  Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= 1f)
        {
            textToReveal.SetActive(true);
        }
        else
        {
            textToReveal.SetActive(false);
        }

    }

    private IEnumerator OnCharacterSwap()
    {  
        player = GameManager.Instance.player;
        yield return null;
    }
}
