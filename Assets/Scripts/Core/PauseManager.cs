using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    private bool _canPause;
    private void Start()
    {
        GlobalEvents.instance.onStageSceneStart += CanPause;
        GlobalEvents.instance.onStageSceneEnd += StageEnded;
    }


    private void OnDestroy()
    {
        GlobalEvents.instance.onStageSceneStart -= CanPause;
        GlobalEvents.instance.onStageSceneEnd -= StageEnded;
    }
    private void StageEnded()
    {
        _canPause = false;
        Time.timeScale = 1;
        IsPaused = false;
    }
    private void CanPause()
    {
        _canPause = true;
    }
    private void CantPause()
    {
        _canPause = false;
    }
    public void TogglePause(Player player)
    {
        //Debug.Log("Pause triggered");
        if (!GUIManager.instance.pauseMenu.tween.TweenActive())
        {

            if (IsPaused && GUIManager.instance.pauseMenu.assignedPlayer == player)
            {
                //Debug.Log("It's Unpause");
                UnPause();
            }
            else if (!IsPaused && _canPause)
            {
                //Debug.Log("It's Pause");
                Pause(player);
            }
        }
        //else { Debug.Log("It's Tweening"); }
    }
    private void Pause(Player player)
    {
        Time.timeScale = 0;
        Debug.Log("Pause for " + player.playerIndex);
        GlobalEvents.instance.onGamePaused?.Invoke(player);
        IsPaused = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        GlobalEvents.instance.onGameUnpaused?.Invoke();
        IsPaused = false;
    }
}
