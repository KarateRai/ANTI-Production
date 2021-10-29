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
    public UnityAction<Player> onPlayerControlChanged;
    public GUIManager.Menus menuType;
    private void Start()
    {
        tween.onEnableComplete += OnEnableAssign;
        tween.onDisableComplete += UnSubscribe;
        tween.onDisableComplete += MenuClosed;
    }
    private void OnDestroy()
    {
        tween.onEnableComplete -= OnEnableAssign;
        tween.onDisableComplete -= UnSubscribe;
        tween.onDisableComplete -= MenuClosed;
    }
    private void MenuClosed()
    {
        GUIManager.instance.MenuClosed(menuType);
    }
    private void OnEnableAssign() 
    {
        if (PlayerManager.instance.players[playerID] != null)
            AssignToPlayer(PlayerManager.instance.players[playerID]);
    }
    public new void AssignToPlayer(Player player)
    {
        if (assignedPlayer != null) { UnSubscribe(); }
        assignedPlayer = player;
        player.eventSystem.playerRoot = menuRoot;
        Subscribe();
        SetSelected(firstSelected);
        onPlayerControlChanged?.Invoke(player);
    }
    public void AssignNoSelect(Player player)
    {
        assignedPlayer = player;
        Debug.Log("Player Assign ID: " + assignedPlayer.playerIndex);
        playerID = assignedPlayer.playerIndex;
        onPlayerControlChanged?.Invoke(player);
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
