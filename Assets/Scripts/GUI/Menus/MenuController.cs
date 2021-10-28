using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuController : ControlledObject
{
    public GameObject menuRoot;
    public GameObject firstSelected;
    public GUITween tween;
    public int playerID;
    [HideInInspector]
    public UnityEvent<Player> onPlayerControlChanged;
    private void Start()
    {
        tween.onEnableComplete.AddListener(() => AssignToPlayer(PlayerManager.instance.players[playerID]));
    }
    private void OnDestroy()
    {
        tween.onEnableComplete.RemoveListener(() => AssignToPlayer(PlayerManager.instance.players[playerID]));
    }
    public new void AssignToPlayer(Player player)
    {
        if (assignedPlayer != null) { UnSubscribe(); }
        assignedPlayer = player;
        player.eventSystem.playerRoot = menuRoot;
        Subscribe();
        SetSelected(firstSelected);
        onPlayerControlChanged.Invoke(player);
    }
    public void AssignNoSelect(Player player)
    {
        assignedPlayer = player;
        playerID = assignedPlayer.PlayerIndex;
        onPlayerControlChanged.Invoke(player);
    }
    public void SetSelected(GameObject toSelect)
    {
        assignedPlayer?.eventSystem.SetSelectedGameObject(toSelect);
    }
    public void Deselect()
    {
        assignedPlayer?.eventSystem.SetSelectedGameObject(null);
    }
    public void OpenMenu()
    {
        tween.Enable();
    }
    public void CloseMenu()
    {
        tween.Disable();
        assignedPlayer.eventSystem.SetSelectedGameObject(null);
    }
}
