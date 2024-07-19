using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberHandler : MonoBehaviour
{
    private Vector3 initialSize;
    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private TextMeshProUGUI numText;
    private Color normalColor;
    private Color critColor;
    private WeaponBase weaponBase;
    private void Awake()
    {
        initialSize = transform.localScale;
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
        numText = GetComponent<TextMeshProUGUI>();
        normalColor = numText.color;
        critColor = Color.red;
    }
    private void OnEnable()
    {
        weaponBase = GameManager.Instance.player.GetComponentInChildren<WeaponBase>();
        transform.localScale = initialSize;

        float randomOffsetX = Random.Range(-0.5f, 0.5f);
        float randomOffsetY = Random.Range(-0.5f, 0.5f);
        float randomRotation = Random.Range(-25f, 25f);

        transform.position = new Vector3(transform.position.x + randomOffsetX, transform.position.y + randomOffsetY, 0f);
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + randomRotation);

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

    private void OnDisable()
    {
        transform.localScale = initialSize;
        transform.eulerAngles = initialRotation;
        transform.position = initialPosition;
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
