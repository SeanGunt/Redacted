using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.cursorSelectedTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.cursorDefaultTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.cursorDefaultTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
