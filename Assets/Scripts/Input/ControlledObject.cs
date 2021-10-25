using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
/// <summary>
/// Put this component on any object that a player should be able to control. 
/// When a player is to gain control of said object, call AssignToPlayer and pass along the controlling Player.
/// Then assign functions to the different events in the inspector to trigger on the events.
/// Functions assigned must accept the InputAction.CallbackContext parameter.
/// </summary>
public class ControlledObject : MonoBehaviour
{
    [HideInInspector]
    public Player assignedPlayer;
    [Header("Interface Actions")]
    public UnityEvent<InputAction.CallbackContext> onNavigate;
    public UnityEvent<InputAction.CallbackContext> onSubmit;
    public UnityEvent<InputAction.CallbackContext> onCancel;
    [Header("Gameplay Actions")]
    public UnityEvent<InputAction.CallbackContext> onMove;
    public UnityEvent<InputAction.CallbackContext> onAim;
    public UnityEvent<InputAction.CallbackContext> onAttack;
    public UnityEvent<InputAction.CallbackContext> onAbilityOne;
    public UnityEvent<InputAction.CallbackContext> onAbilityTwo;
    public UnityEvent<InputAction.CallbackContext> onAbilityThree;
    public UnityEvent<InputAction.CallbackContext> onBuildMode;
    public UnityEvent<InputAction.CallbackContext> onBuild;
    public UnityEvent<InputAction.CallbackContext> onPause;
    public void AssignToPlayer(Player player)
    {
        assignedPlayer = player;
        Subscribe();
    }
    private void OnDestroy()
    {
        UnSubscribe();
    }
    private void Subscribe()
    {
        //Interfacce Actions:
        assignedPlayer.inputProcessor.onNavigate.AddListener((ctx) => onNavigate.Invoke(ctx));
        assignedPlayer.inputProcessor.onSubmit.AddListener((ctx) => onSubmit.Invoke(ctx));
        assignedPlayer.inputProcessor.onCancel.AddListener((ctx) => onCancel.Invoke(ctx));
        //Gameplay Actions:
        assignedPlayer.inputProcessor.onMove.AddListener((ctx) => onMove.Invoke(ctx));
        assignedPlayer.inputProcessor.onAim.AddListener((ctx) => onAim.Invoke(ctx));
        assignedPlayer.inputProcessor.onAttack.AddListener((ctx) => onAttack.Invoke(ctx));
        assignedPlayer.inputProcessor.onAbilityOne.AddListener((ctx) => onAbilityOne.Invoke(ctx));
        assignedPlayer.inputProcessor.onAbilityTwo.AddListener((ctx) => onAbilityTwo.Invoke(ctx));
        assignedPlayer.inputProcessor.onAbilityThree.AddListener((ctx) => onAbilityThree.Invoke(ctx));
        assignedPlayer.inputProcessor.onBuildMode.AddListener((ctx) => onBuildMode.Invoke(ctx));
        assignedPlayer.inputProcessor.onBuild.AddListener((ctx) => onBuild.Invoke(ctx));
    }
    private void UnSubscribe()
    {
        if (assignedPlayer != null)
        {
            //Interfacce Actions:
            assignedPlayer.inputProcessor.onNavigate.RemoveListener((ctx) => onNavigate.Invoke(ctx));
            assignedPlayer.inputProcessor.onSubmit.RemoveListener((ctx) => onSubmit.Invoke(ctx));
            assignedPlayer.inputProcessor.onCancel.RemoveListener((ctx) => onCancel.Invoke(ctx));
            //Gameplay Actions:
            assignedPlayer.inputProcessor.onMove.RemoveListener((ctx) => onMove.Invoke(ctx));
            assignedPlayer.inputProcessor.onAim.RemoveListener((ctx) => onAim.Invoke(ctx));
            assignedPlayer.inputProcessor.onAttack.RemoveListener((ctx) => onAttack.Invoke(ctx));
            assignedPlayer.inputProcessor.onAbilityOne.RemoveListener((ctx) => onAbilityOne.Invoke(ctx));
            assignedPlayer.inputProcessor.onAbilityTwo.RemoveListener((ctx) => onAbilityTwo.Invoke(ctx));
            assignedPlayer.inputProcessor.onAbilityThree.RemoveListener((ctx) => onAbilityThree.Invoke(ctx));
            assignedPlayer.inputProcessor.onBuildMode.RemoveListener((ctx) => onBuildMode.Invoke(ctx));
            assignedPlayer.inputProcessor.onBuild.RemoveListener((ctx) => onBuild.Invoke(ctx));
        }
    }
}
