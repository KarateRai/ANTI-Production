using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class StageSettingsMenu : MenuNavExtras
{
    public TMP_Text difficultyButtonText;
    public Color easyColor, normalColor, hardColor, corruptColor;

    private void Start()
    {
        difficultyButtonText.text = GetDifficultyText(GameManager.instance.gameDifficulty);
        difficultyButtonText.color = GetDifficultyColor(GameManager.instance.gameDifficulty);
    }
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(EndOfFrameReturn());
        }
    }
    IEnumerator EndOfFrameReturn()
    {
        yield return new WaitForEndOfFrame();
        ReturnToTeamMenu();
    }
    private void ReturnToTeamMenu()
    {
        GUIManager.instance.CloseMenu("STAGESETTINGS_MENU");
        GUIManager.instance.ChangeToScene("TeamScene");
    }
    protected override void OnNavLeft()
    {
        switch (selected.name)
        {
            case "ArrowDifficultyButton":
                EditDifficulty(-1);
                OnNavLeftOrRight?.Invoke();
                break;
        }
        difficultyButtonText.text = GetDifficultyText(GameManager.instance.gameDifficulty);
        difficultyButtonText.color = GetDifficultyColor(GameManager.instance.gameDifficulty);
    }
    protected override void OnNavRight()
    {
        switch (selected.name)
        {
            case "ArrowDifficultyButton":
                EditDifficulty(1);
                OnNavLeftOrRight?.Invoke();
                break;
        }
        difficultyButtonText.text = GetDifficultyText(GameManager.instance.gameDifficulty);
        difficultyButtonText.color = GetDifficultyColor(GameManager.instance.gameDifficulty);
    }

    private void EditDifficulty(int value)
    {
        int newValue = GameManager.instance.gameDifficulty += value;
        int sendValue = 2;
        switch (value)
        {
            case -1:
                if ( newValue < 1) { sendValue = 4; }
                else { sendValue = newValue; }
                break;
            case 1:
                if (newValue > 4) { sendValue = 1; }
                else { sendValue = newValue; }
                break;
        }
        GameManager.instance.gameDifficulty = sendValue;
    }
    private string GetDifficultyText(int setting)
    {
        switch (setting)
        {
            case 1:
                return "Easy";
            case 2:
                return "Normal";
            case 3:
                return "Hard";
            case 4:
                return "Corrupt";
            default:
                return "Normal";
        }
    }
    private Color GetDifficultyColor(int setting)
    {
        switch (setting)
        {
            case 1:
                return easyColor;
            case 2:
                return normalColor;
            case 3:
                return hardColor;
            case 4:
                return corruptColor;
            default:
                return normalColor;
        }
    }
}
