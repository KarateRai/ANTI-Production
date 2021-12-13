using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int ownerID;
    public GUITween tween;
    public SmoothFillBar healthBar;
    public SmoothFillBar powerUpBar;
    public Image playerIcon;
    public TMP_Text playerIconText;
    public Image roleIcon;
    public AbilityIcon actionOneIcon;
    public AbilityIcon actionTwoIcon;
    public AbilityIcon movementIcon;
    public AbilityIcon towerIcon;
    private Player _player;
    private PlayerRole _playerRole;
    public PlayerController _playerCharacter;
    public CanvasGroup defaultGroup;
    public CanvasGroup buildGroup;
    public TextMeshProUGUI buildGroupText;
    public CanvasGroup deadGroup;
    public int numTowersAvailable;
    public int numMaxTowers;
    public bool isBuildMode;
    
    public enum DisplayGroups
    {
        DEFAULT,
        BUILD,
        DEAD
    }
    private void Start()
    {
        SetDisplayGroup(DisplayGroups.DEFAULT);
    }
    private void Update()
    {
        if (GameManager.instance.sceneLoader.activeScene.name == "StageOne" && _playerCharacter != null)
        {
            OnHealthUpdated(_playerCharacter.stats.GetHPP());
            OnCooldownUpdated(0, _playerCharacter.unitAbilities.GetCooldown(0));
            OnCooldownUpdated(1, _playerCharacter.unitAbilities.GetCooldown(1));
            OnCooldownUpdated(2, _playerCharacter.unitAbilities.GetCooldown(2));
        }
        if (isBuildMode)
        {
            buildGroupText.text = "Build mode - " + numTowersAvailable + " / " + numMaxTowers;
        }
    }
    public void FetchPlayerData()
    {
        if (PlayerManager.instance.players.Any(p => p.playerIndex == ownerID))
        {
            _player = PlayerManager.instance.players[ownerID];
            _playerRole = PlayerManager.instance.GetPlayerRole(_player.playerChoices.role);
            SetupIcons();
            foreach(PlayerController pChar in FindObjectOfType<RunTimeGameLogic>().players)
            {
                if (pChar.player.playerIndex == ownerID)
                {
                    _playerCharacter = pChar;
                }
            }
        }
    }
    public void SetDisplayGroup(DisplayGroups group)
    {
        switch (group)
        {
            case DisplayGroups.DEFAULT:
                isBuildMode = false;
                defaultGroup.alpha = 1;
                buildGroup.alpha = 0;
                deadGroup.alpha = 0;
                break;
            case DisplayGroups.BUILD:
                isBuildMode = true;
                defaultGroup.alpha = 0;
                buildGroup.alpha = 1;
                deadGroup.alpha = 0;
                break;
            case DisplayGroups.DEAD:
                isBuildMode = false;
                defaultGroup.alpha = 0;
                buildGroup.alpha = 0;
                deadGroup.alpha = 1;
                break;
        }
    }
    public void SetNumTowers(int towersAvailable, int maxNumTowers)
    {
        numTowersAvailable = maxNumTowers - towersAvailable;
        numMaxTowers = maxNumTowers;
    }
    private void SetupIcons()
    {
        roleIcon.sprite = _playerRole.roleIcon;
        actionOneIcon.SetupIcon(_playerRole.abilities[0].cooldownTime, _playerRole.abilities[0].abilityIcon);
        actionTwoIcon.SetupIcon(_playerRole.abilities[1].cooldownTime, _playerRole.abilities[1].abilityIcon);
        movementIcon.SetupIcon(_playerRole.abilities[2].cooldownTime, _playerRole.abilities[2].abilityIcon);
        playerIcon.color = PlayerManager.instance.GetColor(PlayerManager.instance.GetPlayerByID(ownerID).playerChoices.outfit);
    }
    public void OnHealthUpdated(int hpp)
    {
        healthBar.UpdateValues(hpp);
    }
    public void OnCooldownUpdated(int abilityIndex, float seconds)
    {
        switch (abilityIndex)
        {
            case 0: //AbilityOne
                actionOneIcon.UpdateLayout(seconds);
                break;
            case 1: //AbilityTwo
                actionTwoIcon.UpdateLayout(seconds);
                break;
            case 2: //MovementAbility
                movementIcon.UpdateLayout(seconds);
                break;
        }
    }
}
