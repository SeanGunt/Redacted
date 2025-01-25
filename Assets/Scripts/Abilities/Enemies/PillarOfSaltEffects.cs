using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarOfSaltEffects : MonoBehaviour
{
    [SerializeField] private SpriteRenderer pillarRenderer;
    [SerializeField] private AudioClip pillarOfSaltAudioClip;
    private void Awake()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        SFXManager.instance.PlayOneShotAtPoint(transform.position, pillarOfSaltAudioClip);
        yield return new WaitForSeconds(1f);
        while (pillarRenderer.color.a >= 0f)
        {
            pillarRenderer.color -= new Color(0f, 0f, 0f, Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
