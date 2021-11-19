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
    public SmoothFillBar HealthBar;
    public SmoothFillBar PowerUpBar;
    public Image PlayerIcon;
    public TMP_Text PlayerIconText;
    public Image RoleIcon;
    public AbilityIcon ActionOneIcon;
    public AbilityIcon ActionTwoIcon;
    public AbilityIcon MovementIcon;
    public AbilityIcon TowerIcon;
    private Player _player;
    private PlayerRole _playerRole;
    public PlayerController _playerCharacter;
    
    private void Update()
    {
        if (GameManager.instance.sceneLoader.activeScene.name == "StageOne")
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
            //check player's role to extract icons etc.
            foreach(PlayerController pChar in FindObjectOfType<RunTimeGameLogic>().players)
            {
                if (pChar.player.playerIndex == ownerID)
                {
                    _playerCharacter = pChar;
                }
            }
        }
    }
    private void SetupIcons()
    {
        ActionOneIcon.SetupIcon(_playerRole.abilities[0].cooldownTime, _playerRole.abilities[0].abilityIcon);
        ActionTwoIcon.SetupIcon(_playerRole.abilities[1].cooldownTime, _playerRole.abilities[1].abilityIcon);
        MovementIcon.SetupIcon(_playerRole.abilities[2].cooldownTime, _playerRole.abilities[2].abilityIcon);
    }
    public void OnHealthUpdated(int hpp)
    {
        HealthBar.UpdateValues(hpp);
    }
    public void OnCooldownUpdated(int abilityIndex, float seconds)
    {
        switch (abilityIndex)
        {
            case 0: //AbilityOne
                ActionOneIcon.UpdateLayout(seconds);
                break;
            case 1: //AbilityTwo
                ActionTwoIcon.UpdateLayout(seconds);
                break;
            case 2: //MovementAbility
                MovementIcon.UpdateLayout(seconds);
                break;
        }
    }
}
