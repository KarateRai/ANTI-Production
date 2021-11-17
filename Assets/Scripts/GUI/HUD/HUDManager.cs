using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public PlayerHUD[] playerHUDs;
    public GUITween corruptionTween;
    public SmoothFillBar corruptionBar;
    bool isActive;

    private void Update()
    {
        if (GameManager.instance.sceneLoader.activeScene.name == "StageOne")
        {
            if (isActive && GameManager.instance.pauseManager.IsPaused && !corruptionTween.TweenActive()) { DisableHUD(); }
            else if (!isActive && !GameManager.instance.pauseManager.IsPaused && !corruptionTween.TweenActive()) { EnableHUD(); }
        }
        else if (isActive && !corruptionTween.TweenActive()) { DisableHUD(); }
    }
    public void EnableHUD()
    {
        foreach (Player p in PlayerManager.instance.players)
        {
            playerHUDs[p.playerIndex]?.FetchPlayerData();
            playerHUDs[p.playerIndex]?.tween.Enable();
        }
        corruptionTween.Enable();
        isActive = true;
    }

    public void DisableHUD()
    {
        for (int i = 0; i < playerHUDs.Length; i++)
        {
            playerHUDs[i]?.tween.Disable();
        }
        isActive = false;
        corruptionTween.Disable();
    }
}
