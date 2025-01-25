using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseHoverSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.cursorSelectedTexture, Vector2.zero, CursorMode.Auto);
        PauseMenu.Instance.audioSource.PlayOneShot(PauseMenu.Instance.menuHoverAudioClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.cursorDefaultTexture, Vector2.zero, CursorMode.Auto);
    }
}
