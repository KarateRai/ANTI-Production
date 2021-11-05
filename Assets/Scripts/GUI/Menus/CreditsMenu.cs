using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsMenu : MenuNavExtras
{
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToStart();
        }
    }
    public void ReturnToStart()
    {
        GUIManager.instance.CloseMenu("CREDITS_MENU");
        GUIManager.instance.OpenMenu("START_MENU");
    }
}
