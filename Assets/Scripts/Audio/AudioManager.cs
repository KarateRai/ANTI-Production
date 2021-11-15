using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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
