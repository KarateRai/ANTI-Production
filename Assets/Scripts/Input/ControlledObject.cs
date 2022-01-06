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
    public UnityEvent<InputAction.CallbackContext> onDeconstruct;
    public UnityEvent<InputAction.CallbackContext> onPause;
    //private int subBalance;

    private void OnDestroy()
    {
        UnSubscribe();
    }
    
    public void AssignToPlayer(Player player)
    {
        assignedPlayer = player;
        Subscribe();
    }
    
    protected void Subscribe()
    {
        if (assignedPlayer != null)
        {
            //subBalance++;
            //Debug.Log("Player " + assignedPlayer.playerIndex + " subscribed to " + gameObject.name + ". Balance at: " + subBalance);
            assignedPlayer.SubscribeTo(this);
        }
    }


    protected void UnSubscribe()
    {
        if (assignedPlayer != null /*&& subBalance > 0*/)
        {
            //subBalance--;
            //Debug.Log("Player " + assignedPlayer.playerIndex + " unsubscribed from " + gameObject.name + ". Balance at: " + subBalance);
            assignedPlayer.UnsubscribeFrom(this);
        }
    }
}
