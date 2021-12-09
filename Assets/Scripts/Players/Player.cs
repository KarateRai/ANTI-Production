using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(InputDelegator))]
public class Player : MonoBehaviour
{
    public MultiplayerEventSystem eventSystem;
    public PlayerInput playerInput;
    public InputDelegator inputDelegator;
    public PlayerChoices playerChoices;
    public UnityEvent playerRemoved;
    public UnityAction controlsSwapped;
    public List<ControlledObject> controlledObjects;
    public InputAction inputRefSubmit, inputRefCancel, inputRefBuildMode, inputRefAltBuildMode, inputRefBuild, inputRefDeconstruct;
    public string inputSubmitDefaultPath, inputCancelDefaultPath, inputBuildModeDefaultPath, inputAltBuildModeDefaultPath, inputBuildDefaultPath, inputDeconstructDefaultPath;
    public int playerIndex;
    public bool isReady;
    
    private void Start()
    {
        controlledObjects = new List<ControlledObject>();
        playerInput.uiInputModule = GetComponentInChildren<InputSystemUIInputModule>();
        
        inputRefSubmit = playerInput.actions["Interface/Submit"];
        inputRefCancel = playerInput.actions["Interface/Cancel"];
        inputRefBuildMode = playerInput.actions["Gameplay/BuildMode"];
        inputRefAltBuildMode = playerInput.actions["Gameplay/AltBuildMode"];
        inputRefBuild = playerInput.actions["Gameplay/Build"];
        inputRefDeconstruct = playerInput.actions["Gameplay/Deconstruct"];

        inputSubmitDefaultPath = inputRefSubmit.bindings[0].path;
        inputCancelDefaultPath = inputRefCancel.bindings[0].path;
        inputBuildModeDefaultPath = inputRefBuildMode.bindings[0].path;
        inputAltBuildModeDefaultPath = inputRefAltBuildMode.bindings[0].path;
        inputBuildDefaultPath = inputRefBuild.bindings[0].path;
        inputDeconstructDefaultPath = inputRefDeconstruct.bindings[0].path;
    }
    private void OnDestroy()
    {
        playerRemoved?.Invoke();
    }
    public void PressedPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inputDelegator.inputActive == true)
            {
                GameManager.instance.TryToPause(this);
            }
        }
    }
    public void SubscribeTo(ControlledObject controlledObject)
    {
        controlledObjects.Add(controlledObject);
    }
    public void UnsubscribeFrom(ControlledObject controlledObject)
    {
        controlledObjects.Remove(controlledObject);
    }
    public void SetInterfaceRoot(GameObject newRoot)
    {
        eventSystem.playerRoot = newRoot;
    }

    internal void UpdateChoices(PlayerChoices _playerChoices)
    {
        playerChoices.outfit = _playerChoices.outfit;
        playerChoices.role = _playerChoices.role;
        playerChoices.weapon = _playerChoices.weapon;
        playerChoices.tower = _playerChoices.tower;
        //Debug.Log("Update: Old: " + playerChoices.controls.ToString() + " New: " + _playerChoices.controls.ToString());
        playerChoices.controls = _playerChoices.controls;
        StartCoroutine(DelayedButtonSwap());
    }
    public void SetControlMap(PlayerManager.InputStates inputState)
    {
        StartCoroutine(DelayedControlChange(inputState));
    }
    IEnumerator DelayedButtonSwap()
    {
        yield return new WaitForEndOfFrame();
        switch (playerChoices.controls)
        {
            case PlayerChoices.ControlsChoice.MAP_A:
                //Debug.Log("Removing overrides");

                inputRefSubmit.RemoveAllBindingOverrides();
                inputRefCancel.RemoveAllBindingOverrides();
                inputRefBuildMode.RemoveAllBindingOverrides();
                inputRefBuild.RemoveAllBindingOverrides();
                inputRefDeconstruct.RemoveAllBindingOverrides();
                break;
            case PlayerChoices.ControlsChoice.MAP_B:
                //Debug.Log("Applying overrides");
                inputRefSubmit.ApplyBindingOverride(inputCancelDefaultPath);
                inputRefCancel.ApplyBindingOverride(inputSubmitDefaultPath);
                inputRefBuildMode.ApplyBindingOverride(inputAltBuildModeDefaultPath);
                inputRefBuild.ApplyBindingOverride(inputDeconstructDefaultPath);
                inputRefDeconstruct.ApplyBindingOverride(inputBuildDefaultPath);
                break;
        }
    }
    IEnumerator DelayedControlChange(PlayerManager.InputStates inputState)
    {
        yield return new WaitForEndOfFrame();

        switch (inputState)
        {
            case PlayerManager.InputStates.INTERFACE:
                playerInput.SwitchCurrentActionMap("Interface");
                break;
            case PlayerManager.InputStates.GAMEPLAY:
                playerInput.SwitchCurrentActionMap("Gameplay");
                break;
        }

    }

    public void ActivateInput(bool value)
    {
        inputDelegator.inputActive = value;
    }
}
