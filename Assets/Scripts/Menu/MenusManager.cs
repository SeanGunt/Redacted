using UnityEngine;
using UnityEngine.InputSystem;

public class MenusManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Update()
    {
        // Closes Shop
        if (ShopUI.Instance.IsShopOpen() && playerInput.actions["Pause"].triggered)
        {
            ShopUI.Instance.CloseUI();
            return;
        }
        // Pauses Game
        if (!PauseMenu.Instance.IsPauseMenuOpen() && playerInput.actions["Pause"].triggered && !ShopUI.Instance.IsShopOpen())
        {
            PauseMenu.Instance.Pause();
            return;
        }
        // Unpauses Game
        else if (PauseMenu.Instance.IsPauseMenuOpen() && playerInput.actions["Pause"].triggered && !ShopUI.Instance.IsShopOpen())
        {
            PauseMenu.Instance.UnPause();
            return;
        }
    }

    public void GetPlayerInput()
    {
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }
}
