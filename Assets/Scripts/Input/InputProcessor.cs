using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// In order to distribute orders from multiple players, this class triggers events for input actions
/// made by a specific player, which ControlledObject can subscribe to by being assigned a Player.
/// </summary>
public class InputProcessor : MonoBehaviour
{
    #region InterfaceActions
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onNavigate;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onSubmit;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onCancel;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onUnpause;
    public void OnNavigate(InputAction.CallbackContext context) { onNavigate?.Invoke(context); }
    public void OnSubmit(InputAction.CallbackContext context) { onSubmit?.Invoke(context); }
    public void OnCancel(InputAction.CallbackContext context) { onCancel?.Invoke(context); }
    public void OnUnpause(InputAction.CallbackContext context) { onUnpause?.Invoke(context); }
    #endregion
    #region GameplayActions
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onMove;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onAim;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onAttack;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onAbilityOne;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onAbilityTwo;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onAbilityThree;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onBuildMode;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onBuild;
    [HideInInspector]
    public UnityEvent<InputAction.CallbackContext> onPause;
    public void OnMove(InputAction.CallbackContext context) { onMove?.Invoke(context); }
    public void OnAim(InputAction.CallbackContext context) { onAim?.Invoke(context); }
    public void OnAttack(InputAction.CallbackContext context) { onAttack?.Invoke(context); }
    public void OnAbilityOne(InputAction.CallbackContext context) { onAbilityOne?.Invoke(context); }
    public void OnAbilityTwo(InputAction.CallbackContext context) { onAbilityTwo?.Invoke(context); }
    public void OnAbilityThree(InputAction.CallbackContext context) { onAbilityThree?.Invoke(context); }
    public void OnBuildMode(InputAction.CallbackContext context) { onBuildMode?.Invoke(context); }
    public void OnBuild(InputAction.CallbackContext context) { onBuild?.Invoke(context); }
    public void OnPause(InputAction.CallbackContext context) { onPause?.Invoke(context); }
    #endregion

}
