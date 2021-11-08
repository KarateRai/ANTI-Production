using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// In order to distribute orders from multiple players, this class triggers events for input actions
/// made by a specific player, which ControlledObject can "subscribe" to by being assigned a Player.
/// </summary>
public class InputDelegator : MonoBehaviour
{

    [SerializeField] Player player;
    #region DirectInstructions
    public void OnNavigate(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects){ co.onNavigate?.Invoke(context); } }
    public void OnSubmit(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onSubmit?.Invoke(context); } }
    public void OnCancel(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onCancel?.Invoke(context); } }
    public void OnUnpause(InputAction.CallbackContext context) { player.PressedPause(context); }
    public void OnMove(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onMove?.Invoke(context); } }
    public void OnAim(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onAim?.Invoke(context); } }
    public void OnAttack(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onAttack?.Invoke(context); } }
    public void OnAbilityOne(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onAbilityOne?.Invoke(context); } }
    public void OnAbilityTwo(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onAbilityTwo?.Invoke(context); } }
    public void OnAbilityThree(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onAbilityThree?.Invoke(context); } }
    public void OnBuildMode(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onBuildMode?.Invoke(context); } }
    public void OnBuild(InputAction.CallbackContext context) { foreach (ControlledObject co in player.controlledObjects) { co.onBuild?.Invoke(context); } }
    public void OnPause(InputAction.CallbackContext context) { player.PressedPause(context); }
    #endregion
}
