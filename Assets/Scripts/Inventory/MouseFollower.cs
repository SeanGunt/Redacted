using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera mainCamera;
    private PlayerInput playerInput;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInput = GetComponentInParent<PlayerInput>();
        Toggle(false);
    }

    private void Update()
    {
        GetCurrentMousePosition();
    }

    public void Toggle(bool val)
    {
        GetCurrentMousePosition();
        gameObject.SetActive(val);
    }

    private void GetCurrentMousePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, playerInput.actions["PointerPosition"].ReadValue<Vector2>(), canvas.worldCamera, out Vector2 position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
