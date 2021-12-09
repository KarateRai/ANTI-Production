using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuNavExtras
{
    public MenuController menuController;
    public GameObject controlsButton;
    public GameObject controlsLayout1;
    public GameObject controlsLayout2;
    public TMP_Text controlButtonText;
    private GameObject activeLayout;
    private GameObject inactiveLayout;

    private void Start()
    {
        GlobalEvents.instance.onGameUnpaused += DisableMapGraphics;
    }
    public void OnBegin()
    {
        if (menuController.assignedPlayer != null)
        {
            controlButtonText.text = menuController.assignedPlayer.playerChoices.ControlsText();
            UpdateGraphic();
        }
    }
    private void DisableMapGraphics()
    {
        controlsLayout1.SetActive(false);
        controlsLayout2.SetActive(false);
    }
    public void OnEnd()
    {
        if (menuController.assignedPlayer != null)
        {
            UpdateGraphic();
        }
    }
    public new void ChangeSelected(GameObject toSelect)
    {
        selected = toSelect;
        UpdateGraphic();
    }
    protected override void OnNavLeft()
    {
        switch (selected.name)
        {
            case "ArrowButtonControls":
                EditControls(-1);
                Debug.Log("NavRight");
                OnNavLeftOrRight?.Invoke();
                break;
        }
        controlButtonText.text = menuController.assignedPlayer.playerChoices.ControlsText();
        UpdateGraphic();
    }


    protected override void OnNavRight()
    {
        switch (selected.name)
        {
            case "ArrowButtonControls":
                EditControls(1);
                Debug.Log("NavRight");
                OnNavLeftOrRight?.Invoke();
                break;
        }
        controlButtonText.text = menuController.assignedPlayer.playerChoices.ControlsText();
        UpdateGraphic();
    }

    private void EditControls(int v)
    {
        menuController.assignedPlayer.playerChoices.StepEnum(PlayerChoices.EnumTypes.CONTROLS, v);
        menuController.assignedPlayer.UpdateChoices(menuController.assignedPlayer.playerChoices);
    }
    private void UpdateGraphic()
    {
        switch (menuController.assignedPlayer.playerChoices.controls)
        {
            case PlayerChoices.ControlsChoice.MAP_A:
                activeLayout = controlsLayout1;
                inactiveLayout = controlsLayout2;
                break;
            case PlayerChoices.ControlsChoice.MAP_B:
                activeLayout = controlsLayout2;
                inactiveLayout = controlsLayout1;
                break;
        }
        if (selected == controlsButton)
        {
            activeLayout.SetActive(true);
            inactiveLayout.SetActive(false);
        }
        else
        {
            activeLayout.SetActive(false);
            inactiveLayout.SetActive(false);
        }
    }
}
