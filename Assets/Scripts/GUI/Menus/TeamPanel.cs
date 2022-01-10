using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public CanvasGroup controlMapDisplayGroup;
    public Image confirmIcon, cancelIcon;
    private void Start()
    {
        playerChoices = GetComponent<PlayerChoices>();
        menuController.onPlayerControlChanged += ctx => SetPlayer(ctx);
        playerChoices.onChanges += UpdateChanges;
        selectedChanged += ToggleDisplayControlMapping;
    }
    private void OnDestroy()
    {
        menuController.onPlayerControlChanged -= ctx => SetPlayer(ctx);
        playerChoices.onChanges -= UpdateChanges;
        selectedChanged -= ToggleDisplayControlMapping;
    }
    protected override void ExtraUpdate()
    {
        if (GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
        {
            GetComponent<RectTransform>().LeanSetPosX(GUIManager.instance.dressingRoom.mannequins[menuController.playerID].clampGUI.GetPos().x);
        }
        if (player != null)
        {
            if (player.isReady)
            {
                readyText.color = GUIManager.instance.readyColor;
            }
            else
            {
                if (selected.name == "ButtonReady")
                {
                    readyText.color = GUIManager.instance.selectedColor;
                }
                else
                {
                    readyText.color = GUIManager.instance.defaultColor;
                }
            }
        }
    }
    public void ToggleDisplayControlMapping()
    {
        if (selected.name == "ArrowButtonControls")
        {
            DisplayControlMapping(true);
        }
        else
        {
            DisplayControlMapping(false);
        }
    }
    protected void DisplayControlMapping(bool displayOn)
    {
        if (displayOn)
        {
            UpdateMappingIcons();
            controlMapDisplayGroup.alpha = 1;
        }
        else
        {
            controlMapDisplayGroup.alpha = 0;
        }
    }

    private void UpdateMappingIcons()
    {
        if (player != null)
        {
            confirmIcon.sprite = player.playerChoices.GetMappingIcons()[0];
            cancelIcon.sprite = player.playerChoices.GetMappingIcons()[1];
        }
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
                    player = null;
                    break;
                case 2:
                    GUIManager.instance.CloseMenu("TEAM_MENU_3");
                    PlayerManager.instance.SuspendJoining();
                    PlayerManager.instance.RemovePlayer(player);
                    player = null;
                    break;
                case 3:
                    GUIManager.instance.CloseMenu("TEAM_MENU_4");
                    PlayerManager.instance.SuspendJoining();
                    PlayerManager.instance.RemovePlayer(player);
                    player = null;
                    break;
            }
        }
    }
    public void ReadyPlayer()
    {
        if (player.isReady)
        {
            player.isReady = false;
            GUIManager.instance.dressingRoom.MannequinReady(player.playerIndex, false);
            menuController.SetSelected(readyButton);
            readyText.text = "Confirm";
        }
        else
        {
            player.isReady = true;
            GUIManager.instance.dressingRoom.MannequinReady(player.playerIndex, true);
            menuController.Deselect();
            readyText.text = "Ready!";
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
        if (GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
        {
            DressingRoom dRoom = FindObjectOfType<DressingRoom>();
            if (dRoom != null)
            {
                dRoom.UpdateChoices(playerChoices, menuController.playerID);
            }
        }
    }
    private void SendChoices()
    {
        player.UpdateChoices(playerChoices);
        if (GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
        {
            DressingRoom dRoom = FindObjectOfType<DressingRoom>();
            if (dRoom != null)
            {
                dRoom.UpdateChoices(playerChoices, menuController.playerID);
            }
        }
    }
    protected override void OnNavRight()
    {
        switch (selected.name)
        {
            case "ArrowButtonOutfit":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.OUTFIT, 1);
                outfitText.text = playerChoices.OutfitText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonRole":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.ROLE, 1);
                roleText.text = playerChoices.RoleText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonWeapon":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.WEAPON, 1);
                weaponText.text = playerChoices.WeaponText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonTower":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.TOWER, 1);
                towerText.text = playerChoices.TowerText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonControls":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.CONTROLS, 1);
                controlsText.text = playerChoices.ControlsText();
                UpdateMappingIcons();
                OnNavLeftOrRight?.Invoke();
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
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonRole":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.ROLE, -1);
                roleText.text = playerChoices.RoleText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonWeapon":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.WEAPON, -1);
                weaponText.text = playerChoices.WeaponText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonTower":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.TOWER, -1);
                towerText.text = playerChoices.TowerText();
                OnNavLeftOrRight?.Invoke();
                break;
            case "ArrowButtonControls":
                playerChoices.StepEnum(PlayerChoices.EnumTypes.CONTROLS, -1);
                controlsText.text = playerChoices.ControlsText();
                UpdateMappingIcons();
                OnNavLeftOrRight?.Invoke();
                break;
        }
    }
    public void UpdateChanges()
    {
        SendChoices();
    }
}
