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

}
