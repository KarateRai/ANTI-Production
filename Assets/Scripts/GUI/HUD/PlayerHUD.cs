using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int ownerID;
    public GUITween tween;
    public SmoothFillBar HealthBar;
    public SmoothFillBar PowerUpBar;
    public Image PlayerIcon;
    public Image RoleIcon;
    public Image ActionOneIcon;
    public Image ActionTwoIcon;
    public Image MovementIcon;
    private Player _player;
    private void Start()
    {
        GlobalEvents.instance.onStageSceneStart += SetupIcons;
    }

    private void OnDestroy()
    {
        GlobalEvents.instance.onStageSceneStart -= SetupIcons;
    }
    private void SetupIcons()
    {
        if (PlayerManager.instance.players.Any(p => p.playerIndex == ownerID))
        {
            _player = PlayerManager.instance.players[ownerID];
            //check player's role to extract icons etc.
            //also maybe fix the list of players to be less prone to error refs.
        }
    }

    public void OnHealthUpdated(int hpp)
    {
        HealthBar.UpdateValues(hpp);
    }
}
