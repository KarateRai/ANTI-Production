using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(InputProcessor))]
public class Player : MonoBehaviour
{
    public MultiplayerEventSystem eventSystem;
    public PlayerInput playerInput;
    public InputProcessor inputProcessor;
    public PlayerChoices playerChoices;
    public UnityEvent playerRemoved;
    public int PlayerIndex { get { return playerInput.playerIndex; } }
    public bool isReady;
    private void Start()
    {
        playerInput.uiInputModule = GetComponentInChildren<InputSystemUIInputModule>();
        inputProcessor.onPause.AddListener((ctx) => PressedPause(ctx));
        inputProcessor.onUnpause.AddListener((ctx) => PressedPause(ctx));
    }
    private void OnDestroy()
    {
        playerRemoved.Invoke();
        inputProcessor.onPause.RemoveListener((ctx) => PressedPause(ctx));
        inputProcessor.onUnpause.RemoveListener((ctx) => PressedPause(ctx));
    }
    private void PressedPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.instance.TryToPause(this);
        }
    }
    public void SetInterfaceRoot(GameObject newRoot)
    {
        eventSystem.playerRoot = newRoot;
    }

}
