using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public MusicPlayer musicPlayer;
    private float _masterVolume;
    private float _musicVolume;
    private float _effectVolume;
    private FMOD.Studio.Bus _masterBus;
    private FMOD.Studio.Bus _musicBus;
    private FMOD.Studio.Bus _effectBus;
    private FMOD.Studio.EventInstance EffectVolumeTestEvent;
    //variables for intensity/glitchiness etc needed.
    private float _epicness = 0;
    private float _smoothEpicness = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        _masterBus = RuntimeManager.GetBus("bus:/Master");
        _musicBus = RuntimeManager.GetBus("bus:/Master/Music");
        _effectBus = RuntimeManager.GetBus("bus:/Master/Effects");
        EffectVolumeTestEvent = RuntimeManager.CreateInstance("event:/Effects/EffectVolumeTest");
    }
    private void Update()
    {
        CheckMusicVariables();
    }

    private void CheckMusicVariables()
    {
        //todo: set up better checks to reflect game
        
        if (GameManager.instance.sceneLoader.activeScene.name == "StageOne")
        {
            _epicness = 100;
        }
        else if(GameManager.instance.sceneLoader.activeScene.name == "StageSettingsScene")
        {
            _epicness = 30;
        }
        else if(GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
        {
            _epicness = 15;
        }
        else if(GameManager.instance.sceneLoader.activeScene.name == "MenuScene")
        {
            _epicness = 0;
        }
        if (_smoothEpicness < _epicness)
        {
            _smoothEpicness += 0.05f;
        }
        else if (_smoothEpicness > _epicness) 
        {
            _smoothEpicness -= 0.05f;
        }
        else { _smoothEpicness = _epicness; }
        musicPlayer.SetParameter(MusicPlayer.MusicParameters.EPICNESS, _smoothEpicness);
    }

    public void SetInitialVolume(int master, int music, int effects)
    {
        _masterVolume = (float)master / 100f;
        _masterBus.setVolume(_masterVolume);
        _musicVolume = (float)music / 100f;
        _musicBus.setVolume(_musicVolume);
        _effectVolume = (float)effects / 100f;
        _effectBus.setVolume(_effectVolume);
        musicPlayer.Play(); //move this later
    }
    private void OnValuesUpdated()
    {

    }

    public void ChangeVolume(int type, int valuePercent)
    {
        float value = (float)valuePercent / 100f;
        switch (type)
        {
            case 0: //Master Volume
                _masterVolume = value;
                _masterBus.setVolume(_masterVolume);
                //Debug.Log("Master at: "+_masterVolume);
                break;
            case 1: //Music Volume
                _musicVolume = value;
                _musicBus.setVolume(_musicVolume);
                //Debug.Log("Music at: " + _musicVolume);
                break;
            case 2: //Effect Volume
                _effectVolume = value;
                _effectBus.setVolume(_effectVolume);
                EffectVolumeTestEvent.start();
                //Debug.Log("Effects at: " + _effectVolume);
                break;
        }
        OnValuesUpdated();
    }

}
