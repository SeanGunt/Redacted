using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera mainCamera;
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        mainCamera = Camera.main;
        Toggle(false);
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, playerInput.actions["PointerPosition"].ReadValue<Vector2>(), 
        canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}
