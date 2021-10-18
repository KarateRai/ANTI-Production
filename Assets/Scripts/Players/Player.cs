using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(InputProcessor))]
public class Player : MonoBehaviour
{
    public MultiplayerEventSystem eventSystem;
    public PlayerInput playerInput;
    public InputProcessor inputProcessor;
    public int PlayerIndex { get { return playerInput.playerIndex; } }
    
    private void Start()
    {
        playerInput.uiInputModule = GetComponentInChildren<InputSystemUIInputModule>();
        inputProcessor.onPause.AddListener((ctx) => PressedPause(ctx));
    }
    private void OnDestroy()
    {
        inputProcessor.onPause.RemoveListener((ctx) => PressedPause(ctx));
    }
    private void PressedPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Player " + PlayerIndex + " pressed Pause.");
        if (context.canceled)
            Debug.Log("Player " + PlayerIndex + " released Pause.");
        //todo: tell relevant class that player is attempting to pause.
    }
    public void SetInterfaceRoot(GameObject newRoot)
    {
        eventSystem.playerRoot = newRoot;
    }

}
