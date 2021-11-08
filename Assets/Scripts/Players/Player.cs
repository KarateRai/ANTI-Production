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
    public InputDelegator inputProcessor;
    public PlayerChoices playerChoices;
    public UnityEvent playerRemoved;
    public List<ControlledObject> controlledObjects;
    public int playerIndex;
    public bool isReady;
    private void Start()
    {
        controlledObjects = new List<ControlledObject>();
        playerInput.uiInputModule = GetComponentInChildren<InputSystemUIInputModule>();
    }
    private void OnDestroy()
    {
        playerRemoved?.Invoke();
    }
    public void PressedPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.instance.TryToPause(this);
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
        playerChoices.controls = _playerChoices.controls;
        SetControlMap(PlayerManager.InputStates.INTERFACE);
    }
    public void SetControlMap(PlayerManager.InputStates inputState)
    {
        StartCoroutine(DelayedControlChange(inputState));
        //TODO: change it so we re-map confirm and cancel instead of changing interface maps
    }
    IEnumerator DelayedControlChange(PlayerManager.InputStates inputState)
    {
        yield return new WaitForEndOfFrame();

        switch (inputState)
        {
            case PlayerManager.InputStates.INTERFACE:
                //switch (playerChoices.controls)
                //{
                    //case PlayerChoices.ControlsChoice.MAP_A:
                        playerInput.SwitchCurrentActionMap("InterfaceA");
                        //break;
                //    case PlayerChoices.ControlsChoice.MAP_B:
                //        playerInput.SwitchCurrentActionMap("InterfaceB");
                //        break;
                //}
                break;
            case PlayerManager.InputStates.GAMEPLAY:
                playerInput.SwitchCurrentActionMap("Gameplay");
                break;
        }

    }
}
