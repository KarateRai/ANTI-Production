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
    public Image roleIcon;
    public Image buildModeIcon, demolishIcon, buildIcon, combatModeIcon;
    public TMP_Text playerIconText;
    public TMP_Text towerCountText;
    public AbilityIcon actionOneIcon;
    public AbilityIcon actionTwoIcon;
    public AbilityIcon movementIcon;
    public AbilityIcon towerIcon;
    private Player _player;
    private PlayerRole _playerRole;
    private PlayerController _playerCharacter;
    public CanvasGroup defaultGroup;
    public CanvasGroup buildGroup;
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
        
        int newNumTowersAvailable = maxNumTowers - towersAvailable;
        int newNumMaxTowers = maxNumTowers;
        if (numTowersAvailable != newNumTowersAvailable || numMaxTowers != newNumMaxTowers)
        {
            numTowersAvailable = newNumTowersAvailable;
            numMaxTowers = newNumMaxTowers;
            towerCountText.text = 2-numTowersAvailable + "/" + numMaxTowers;
        }
    }
    private void SetupIcons()
    {
        roleIcon.sprite = _playerRole.roleIcon;
        actionOneIcon.SetupIcon(_playerRole.abilities[0].cooldownTime, _playerRole.abilities[0].abilityIcon);
        actionTwoIcon.SetupIcon(_playerRole.abilities[1].cooldownTime, _playerRole.abilities[1].abilityIcon);
        movementIcon.SetupIcon(_playerRole.abilities[2].cooldownTime, _playerRole.abilities[2].abilityIcon);
        playerIcon.color = PlayerManager.instance.GetColor(PlayerManager.instance.GetPlayerByID(ownerID).playerChoices.outfit);
        buildIcon.sprite = _player.playerChoices.GetMappingIcons()[0];
        demolishIcon.sprite = _player.playerChoices.GetMappingIcons()[1];
        buildModeIcon.sprite = _player.playerChoices.GetMappingIcons()[2];
        combatModeIcon.sprite = _player.playerChoices.GetMappingIcons()[2];
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
