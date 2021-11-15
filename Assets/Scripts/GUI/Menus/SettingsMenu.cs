using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class SettingsMenu : MenuNavExtras
{
    public SmoothFillBar volumeMasterBar;
    public SmoothFillBar volumeMusicBar;
    public SmoothFillBar volumeSFXBar;
    public GameObject checkMarkFullscreen;
    [Header("Text objects")]
    public Color selectedColor, defaultColor;
    public TMP_Text fullscreenHeaderText;
    public TMP_Text screenResHeaderText;
    public TMP_Text screenResText;
    public TMP_Text masterVolumeHeaderText;
    public TMP_Text musicVolumeHeaderText;
    public TMP_Text sfxVolumeHeaderText;
    public TMP_Text returnButtonText;
    [Range(0,100)]
    private int _volumeMaster = 100;
    [Range(0, 100)]
    private int _volumeMusic = 100;
    [Range(0, 100)]
    private int _volumeSFX = 100;
    private bool _fullscreen;
    private GameObject prevSelected;
    private Resolution[] resolutions;
    private List<string> resolutionStrings;
    private int currentResIndex;
    private void Start()
    {
        LoadValues();
        GetResolutions();
    }
    private void Update()
    {
        if (_fullscreen && !checkMarkFullscreen.activeInHierarchy) { checkMarkFullscreen.SetActive(true); }
        else if (!_fullscreen && checkMarkFullscreen.activeInHierarchy) { checkMarkFullscreen.SetActive(false); }

        if (selected != prevSelected)
        {
            prevSelected = selected;
            SetColors();
        }
    }

    private void GetResolutions()
    {
        _fullscreen = Screen.fullScreen;
        resolutions = Screen.resolutions;
        resolutionStrings = new List<string>();
        currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string reso = resolutions[i].width+" x "+resolutions[i].height+" @"+resolutions[i].refreshRate+"Hz";
            resolutionStrings.Add(reso);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResIndex = i;
                screenResText.text = resolutionStrings[i];
            }
        }

    }

    private void EditResolution(int v)
    {
        if (currentResIndex+v > resolutions.Length - 1)
        {
            currentResIndex = 0;
        }
        else if (currentResIndex+v < 0)
        {
            currentResIndex = resolutions.Length - 1;
        }
        else
        {
            currentResIndex += v;
        }
        screenResText.text = resolutionStrings[currentResIndex];
        Screen.SetResolution(resolutions[currentResIndex].width, resolutions[currentResIndex].height, _fullscreen);
    }


    private void SetColors()
    {
        switch (selected.name)
        {
            case "ButtonFullscreen":
                fullscreenHeaderText.color = selectedColor;
                screenResHeaderText.color = defaultColor;
                screenResText.color = defaultColor;
                masterVolumeHeaderText.color = defaultColor;
                musicVolumeHeaderText.color = defaultColor;
                sfxVolumeHeaderText.color = defaultColor;
                returnButtonText.color = defaultColor;
                break;
            case "ArrowButtonResolution":
                fullscreenHeaderText.color = defaultColor;
                screenResHeaderText.color = selectedColor;
                screenResText.color = selectedColor;
                masterVolumeHeaderText.color = defaultColor;
                musicVolumeHeaderText.color = defaultColor;
                sfxVolumeHeaderText.color = defaultColor;
                returnButtonText.color = defaultColor;
                break;
            case "ButtonVolumeMaster":
                fullscreenHeaderText.color = defaultColor;
                screenResHeaderText.color = defaultColor;
                screenResText.color = defaultColor;
                masterVolumeHeaderText.color = selectedColor;
                musicVolumeHeaderText.color = defaultColor;
                sfxVolumeHeaderText.color = defaultColor;
                returnButtonText.color = defaultColor;
                break;
            case "ButtonVolumeMusic":
                fullscreenHeaderText.color = defaultColor;
                screenResHeaderText.color = defaultColor;
                screenResText.color = defaultColor;
                masterVolumeHeaderText.color = defaultColor;
                musicVolumeHeaderText.color = selectedColor;
                sfxVolumeHeaderText.color = defaultColor;
                returnButtonText.color = defaultColor;
                break;
            case "ButtonVolumeSFX":
                fullscreenHeaderText.color = defaultColor;
                screenResHeaderText.color = defaultColor;
                screenResText.color = defaultColor;
                masterVolumeHeaderText.color = defaultColor;
                musicVolumeHeaderText.color = defaultColor;
                sfxVolumeHeaderText.color = selectedColor;
                returnButtonText.color = defaultColor;
                break;
            case "ButtonReturn":
                fullscreenHeaderText.color = defaultColor;
                screenResHeaderText.color = defaultColor;
                screenResText.color = defaultColor;
                masterVolumeHeaderText.color = defaultColor;
                musicVolumeHeaderText.color = defaultColor;
                sfxVolumeHeaderText.color = defaultColor;
                returnButtonText.color = selectedColor;
                break;
        }
    }

    private void LoadValues()
    {
        //todo: load settings from file or possibly project settings if unity already tracks this?
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToStart();
        }
    }
    public void ReturnToStart()
    {
        GUIManager.instance.CloseMenu("SETTINGS_MENU");
        GUIManager.instance.OpenMenu("START_MENU");
    }
    public void ToggleFullscreen()
    {
        if (_fullscreen)
        {
            _fullscreen = false;
        }
        else
        {
            _fullscreen = true;
        }
        Screen.fullScreen = _fullscreen;
    }
    protected override void OnNavLeft() 
    {
        Debug.Log(selected.name);
        switch (selected.name)
        {
            case "ArrowButtonResolution":
                EditResolution(-1);
                break;
            case "ButtonVolumeMaster":
                AdjustVolumeMaster(-5);
                break;
            case "ButtonVolumeMusic":
                AdjustVolumeMusic(-5);
                break;
            case "ButtonVolumeSFX":
                AdjustVolumeSFX(-5);
                break;
        }
    }

    protected override void OnNavRight() 
    {
        switch (selected.name)
        {
            case "ArrowButtonResolution":
                EditResolution(1);
                break;
            case "ButtonVolumeMaster":
                AdjustVolumeMaster(5);
                break;
            case "ButtonVolumeMusic":
                AdjustVolumeMusic(5);
                break;
            case "ButtonVolumeSFX":
                AdjustVolumeSFX(5);
                break;

        }
    }

    private void AdjustVolumeMaster(int value)
    {
        switch (value)
        {
            case 5:
                if (_volumeMaster < 100)
                {
                    _volumeMaster += value;
                }
                break;
            case -5:
                if (_volumeMaster > 0)
                {
                    _volumeMaster += value;
                }
                break;
        }
        volumeMasterBar.UpdateValues(_volumeMaster);
    }
    private void AdjustVolumeMusic(int value)
    {
        switch (value)
        {
            case 5:
                if (_volumeMusic < 100)
                {
                    _volumeMusic += value;
                }
                break;
            case -5:
                if (_volumeMusic > 0)
                {
                    _volumeMusic += value;
                }
                break;
        }
        volumeMusicBar.UpdateValues(_volumeMusic);
    }
    private void AdjustVolumeSFX(int value)
    {
        switch (value)
        {
            case 5:
                if (_volumeSFX < 100)
                {
                    _volumeSFX += value;
                }
                break;
            case -5:
                if (_volumeSFX > 0)
                {
                    _volumeSFX += value;
                }
                break;
        }
        volumeSFXBar.UpdateValues(_volumeSFX);
    }
}
