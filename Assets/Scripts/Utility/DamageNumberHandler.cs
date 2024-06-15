using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberHandler : MonoBehaviour
{
    private Vector3 initialSize;
    private TextMeshProUGUI numText;
    private Color normalColor;
    private Color critColor;
    private WeaponBase weaponBase;
    private void Awake()
    {
        initialSize = transform.localScale;
        numText = GetComponent<TextMeshProUGUI>();
        normalColor = numText.color;
        critColor = Color.red;
        weaponBase = GameManager.Instance.player.GetComponentInChildren<WeaponBase>();
    }
    private void OnEnable()
    {
        transform.localScale = initialSize;
        if (weaponBase.wasCriticalHit)
        {
            numText.color = critColor;
        }
        else
        {
            numText.color = normalColor;
        }
        StartCoroutine(HandleShrink());
    }

    private IEnumerator HandleShrink()
    {
        Vector3 startSize = transform.localScale;
        while (transform.localScale.x <= 0.015f)
        {
            startSize.x += Time.deltaTime * 0.025f;
            startSize.y += Time.deltaTime * 0.025f;
            transform.localScale = new Vector3(startSize.x, startSize.y, 0f);
            yield return null;
        }

        StartCoroutine(HandleFade());

        while (transform.localScale.x >= 0.005)
        {
            startSize.x -= Time.deltaTime * 0.01f;
            startSize.y -= Time.deltaTime * 0.01f;
            transform.localScale = new Vector3(startSize.x, startSize.y, 0f);
            yield return null;
        }
    }

    private IEnumerator HandleFade()
    {
        float startAlpha = 1f;
        while (numText.color.a >= 0)
        {
            startAlpha -= Time.deltaTime;
            numText.color = new Color(numText.color.r, numText.color.g, numText.color.b, startAlpha);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
