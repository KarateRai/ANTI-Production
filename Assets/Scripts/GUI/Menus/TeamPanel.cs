using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerChoices))]
public class TeamPanel : MenuNavExtras
{
    public MenuController menuController;
    [HideInInspector]
    public PlayerChoices playerChoices;
    private Player player;
    public TMP_Text outfitText;
    public TMP_Text roleText;
    public TMP_Text weaponText;
    public TMP_Text towerText;
    public TMP_Text controlsText;
    public TMP_Text readyText;
    public GameObject readyButton;
    private void Start()
    {
        playerChoices = GetComponent<PlayerChoices>();
        menuController.onPlayerControlChanged += ctx => SetPlayer(ctx);
        playerChoices.onChanges += UpdateChanges;
    }
    private void OnDestroy()
    {
        menuController.onPlayerControlChanged -= ctx => SetPlayer(ctx);
        playerChoices.onChanges -= UpdateChanges;
    }
    private void SetPlayer(Player _player)
    {
        player = _player;
    }
    /// <summary>
    /// Removes player if they cancel out of their menu during TeamScene. Does not apply to player 1. 
    /// Returns to main menu if player 1 is alone and cancels.
    /// </summary>
    /// <param name="context"></param>
    public void OnCancel(InputAction.CallbackContext context)
    {
        
        if (GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
        {

            if (player == null) { return; }
            if (context.performed)
            {
                StartCoroutine(CancelOut());
            }
        }
    }
    
    IEnumerator CancelOut()
    {
        yield return new WaitForEndOfFrame();
        if (player.isReady)
        {
            ReadyPlayer();
        }
        else
        {
            switch (player.playerIndex)
            {
                case 0:
                    if (PlayerManager.instance.players.Count <= 1)
                    {
                        PlayerManager.instance.JoinOff();
                        GUIManager.instance.CloseMenu("TEAM_MENU_1");
                        GUIManager.instance.ChangeToScene("MenuScene");
                    }
                    break;
                case 1:
                    GUIManager.instance.CloseMenu("TEAM_MENU_2");
                    PlayerManager.instance.SuspendJoining();
                    PlayerManager.instance.RemovePlayer(player);
                    break;
                case 2:
                    GUIManager.instance.CloseMenu("TEAM_MENU_3");
                    PlayerManager.instance.SuspendJoining();
                    PlayerManager.instance.RemovePlayer(player);
                    break;
                case 3:
                    GUIManager.instance.CloseMenu("TEAM_MENU_4");
                    PlayerManager.instance.SuspendJoining();
                    PlayerManager.instance.RemovePlayer(player);
                    break;
            }
        }
    }
    public void ReadyPlayer()
    {
        if (player.isReady)
        {
            player.isReady = false;
            menuController.SetSelected(readyButton);
            readyText.text = "Confirm";
        }
        else
        {
            player.isReady = true;
            menuController.Deselect();
            readyText.text = "Ready!";
            //SendChoices();
            PlayerManager.instance.ReadyCheck();
        }
    }
    public void SetChoices(PlayerChoices newChoices)
    {
        playerChoices.outfit = newChoices.outfit;
        playerChoices.role = newChoices.role;
        playerChoices.weapon = newChoices.weapon;
        playerChoices.tower = newChoices.tower;
        playerChoices.controls = newChoices.controls;
        outfitText.text = playerChoices.OutfitText();
        roleText.text = playerChoices.RoleText();
        weaponText.text = playerChoices.WeaponText();
        towerText.text = playerChoices.TowerText();
        controlsText.text = playerChoices.ControlsText();
        readyText.text = "Confirm";
    }
    private void SendChoices()
    {
        player.UpdateChoices(playerChoices);
        //player.playerChoices.outfit = playerChoices.outfit;
        //player.playerChoices.role = playerChoices.role;
        //player.playerChoices.weapon = playerChoices.weapon;
        //player.playerChoices.tower = playerChoices.tower;
        //player.playerChoices.controls = playerChoices.controls;
    }
    protected override void OnNavRight()
    {
        switch (selected.name)
        {
            case "ArrowButtonOutfit":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.OUTFIT, 1);
                outfitText.text = playerChoices.OutfitText();
                break;
            case "ArrowButtonRole":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.ROLE, 1);
                roleText.text = playerChoices.RoleText();
                break;
            case "ArrowButtonWeapon":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.WEAPON, 1);
                weaponText.text = playerChoices.WeaponText();
                break;
            case "ArrowButtonTower":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.TOWER, 1);
                towerText.text = playerChoices.TowerText();
                break;
            case "ArrowButtonControls":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.CONTROLS, 1);
                controlsText.text = playerChoices.ControlsText();
                break;
        }
    }

    protected override void OnNavLeft()
    {
        switch (selected.name)
        {
            case "ArrowButtonOutfit":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.OUTFIT,-1);
                outfitText.text = playerChoices.OutfitText();
                break;
            case "ArrowButtonRole":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.ROLE, -1);
                roleText.text = playerChoices.RoleText();
                break;
            case "ArrowButtonWeapon":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.WEAPON, -1);
                weaponText.text = playerChoices.WeaponText();
                break;
            case "ArrowButtonTower":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.TOWER, -1);
                towerText.text = playerChoices.TowerText();
                break;
            case "ArrowButtonControls":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.CONTROLS, -1);
                controlsText.text = playerChoices.ControlsText();
                break;
        }
    }
    public void UpdateChanges()
    {
        SendChoices();
    }
}
