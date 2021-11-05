using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsMenu : MenuNavExtras
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
        GUIManager.instance.CloseMenu("SETTINGS_MENU");
        GUIManager.instance.OpenMenu("START_MENU");
    }
    
    protected override void OnNavLeft() { }
    protected override void OnNavRight() { }
}
