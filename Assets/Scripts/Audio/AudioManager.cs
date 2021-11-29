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
    private float _segment = 1;
    private float _glitch = 0;
    private float _charSelect = 0;
    private float _epicness = 0;
    private float _smoothEpicness = 0;
    public bool overrideMusicSettings = false;
    private bool _paused = false;
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
        GlobalEvents.instance.onMenuSceneStart += MenuStart;
        GlobalEvents.instance.onStageSceneStart += StageStart;
        GlobalEvents.instance.onGameOver += GameOver;
    }
    private void OnDestroy()
    {
        GlobalEvents.instance.onMenuSceneStart -= MenuStart;
        GlobalEvents.instance.onStageSceneStart -= StageStart;
        GlobalEvents.instance.onGameOver -= GameOver;
    }
    private void Update()
    {
        CheckMusicVariables();
    }

    private void MenuStart()
    {
        musicPlayer.Play(MusicPlayer.MusicTracks.MENU_TRACK);
    }
    private void StageStart()
    {
        musicPlayer.Play(MusicPlayer.MusicTracks.GAMEPLAY_TRACK);
    }
    private void GameOver()
    {
        musicPlayer.Play(MusicPlayer.MusicTracks.MENU_TRACK);
    }
    private void CheckMusicVariables()
    {
        //todo: set up better checks to reflect game
        if (overrideMusicSettings)
        {
            musicPlayer.SetParameter(MusicPlayer.MusicParameters.CHARSELECT, 1);
        }
        else
        {
            if (GameManager.instance.sceneLoader.activeScene.name == "StageOne")
            {
                _segment = CheckSegment();
                _glitch = CheckGlitch();
            }
            else if(GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
            {
                _charSelect = 1;
            }
            else if(GameManager.instance.sceneLoader.activeScene.name == "MenuScene")
            {
                _charSelect = 0;
            }
            musicPlayer.SetParameter(MusicPlayer.MusicParameters.CHARSELECT, _charSelect);
            musicPlayer.SetParameter(MusicPlayer.MusicParameters.SEGMENT, _segment);
            musicPlayer.SetParameter(MusicPlayer.MusicParameters.GLITCH, _glitch);

        }
    }

    private float CheckGlitch()
    {
        //Check state of corruption and return float between 0-1f 
        return 0f;
    }

    private float CheckSegment()
    {
        //Check gameplay state and judge what segment to play between 2-10. 2-10 
        return (float)GameManager.instance.intensity;
    }

    public void SetInitialVolume(int master, int music, int effects)
    {
        _masterVolume = (float)master / 100f;
        _masterBus.setVolume(_masterVolume);
        _musicVolume = (float)music / 100f;
        _musicBus.setVolume(_musicVolume);
        _effectVolume = (float)effects / 100f;
        _effectBus.setVolume(_effectVolume);
        //musicPlayer.Play(MusicPlayer.MusicTracks.MENU_TRACK); //move this later?
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
