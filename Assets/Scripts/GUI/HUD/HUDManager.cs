using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public PlayerHUD[] playerHUDs;
    public GUITween corruptionTween;
    public SmoothFillBar corruptionBar;
    public GameObject healthBars;
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
    public void UpdateCorruption(int percentage)
    {
        corruptionBar.UpdateValues(percentage);
        float glitchAmount = (1f+(((float)percentage) / 100f))/2f;
        //Debug.Log("Glitch amount: " + glitchAmount);
        StartCoroutine(AudioManager.instance.musicPlayer.GlitchOut(glitchAmount, 2));
    }
}
