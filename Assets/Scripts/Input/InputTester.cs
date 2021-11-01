using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Use only for on the fly testing.
/// Add all ControlledObject gameobjects you wish to controlto the list in the interface.
/// Press any button on the controller device during runtime to start controlling the ControlledObjects.
/// </summary>
public class InputTester : MonoBehaviour
{
    public List<ControlledObject> controlledObjects;
    [HideInInspector]
    public Player fakePlayer;
    public PlayerInputManager inputManager;
    private void Assign()
    {
        for (int i = 0; i < controlledObjects.Count; i++)
        {
            Debug.Log("Assigned: " + controlledObjects[i].name + " to " + fakePlayer.name);
            controlledObjects[i].AssignToPlayer(fakePlayer);
        }
    }
    public void HandlePlayerJoin(PlayerInput pi)
    {
        pi.transform.SetParent(transform);
        Player newPlayer = pi.gameObject.GetComponent<Player>();
        newPlayer.playerInput.SwitchCurrentActionMap("Gameplay");
        fakePlayer = newPlayer;
        Debug.Log("Player ID: " + pi.playerIndex + " has joined.");
        inputManager.DisableJoining();
        StartCoroutine(DelayedAssign());
    }
    IEnumerator DelayedAssign()
    {
        yield return new WaitForEndOfFrame();
        Assign();
    }
}
