using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public PlayerHUD[] playerHUDs;
    public GUITween corruptionTween;
    public SmoothFillBar corruptionBar;
    public GameObject trackedObjects;
    bool isActive;

    private void Update()
    {
        if (GameManager.instance.sceneLoader.activeScene.name == "StageOne")
        {
            if (isActive && GameManager.instance.pauseManager.IsPaused && !corruptionTween.TweenActive()) { DisableAllHUD(); }
            else if (!isActive && !GameManager.instance.pauseManager.IsPaused && !corruptionTween.TweenActive()) { EnableAllHUD(); }
        }
        else if (isActive && !corruptionTween.TweenActive()) { DisableAllHUD(); }
    }
    public void EnableAllHUD()
    {
        foreach (Player p in PlayerManager.instance.players)
        {
            EnableHUD(p.playerIndex);
        }
        corruptionTween.Enable();
        isActive = true;
    }
    public void EnableHUD(int index)
    {
        playerHUDs[index]?.FetchPlayerData();
        playerHUDs[index]?.tween.Enable();
    }
    public void DisableAllHUD()
    {
        for (int i = 0; i < playerHUDs.Length; i++)
        {
            DisableHUD(i);
        }
        isActive = false;
        corruptionTween.Disable();
    }
    public void DisableHUD(int index)
    {
        playerHUDs[index]?.tween.Disable();
    }
    public void UpdateCorruption(int percentage)
    {
        corruptionBar.UpdateValues(percentage);
        float glitchAmount = (1f+(((float)percentage) / 100f))/2f;
        //Debug.Log("Glitch amount: " + glitchAmount);
        StartCoroutine(AudioManager.instance.musicPlayer.GlitchOut(glitchAmount, 2));
    }
}
