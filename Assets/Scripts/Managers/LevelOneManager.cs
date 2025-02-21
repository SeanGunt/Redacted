using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{
    public static LevelOneManager instance;
    [SerializeField] private MapUpdate mapUpdate;
    private SpriteRenderer[] worldObjectRenderers;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartVoidification();
    }

    public void StartVoidification()
    {
        StartCoroutine(VoidifyMap());
    }

    private IEnumerator VoidifyMap()
    {
        yield return new WaitForSeconds(1f);
        worldObjectRenderers = mapUpdate.worldObjectHolder.GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(worldObjectRenderers.Length);
        float g = 255f;
        while (g > 80f)
        {
            g -= Time.deltaTime / 2f;
            for (int i = 0; i < worldObjectRenderers.Length; i++)
            {
                worldObjectRenderers[i].color -= new Color(0f, Time.deltaTime / 2f, 0f, 0f);
            }
            mapUpdate.baseLayer.color -= new Color(0f, Time.deltaTime / 2f, 0f, 0f);
            mapUpdate.collidableLayer.color -= new Color(0f, Time.deltaTime / 2f, 0f, 0f);
            yield return null;
        }
    }
}
