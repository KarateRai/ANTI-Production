using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class MusicPlayer : MonoBehaviour
{

    public StudioEventEmitter menuTrack, gameplayTrack;
    //[Range(0, 100f)]
    //private float _epicness = 0;
    [Range(0,1)]
    private float _charSelect = 0;
    [Range(0,1f)]
    private float _glitch = 0;
    private float _segment = 1;
    public enum MusicParameters
    {
        //EPICNESS,
        CHARSELECT,
        GLITCH,
        SEGMENT
    }
    public enum MusicTracks
    {
        MENU_TRACK,
        GAMEPLAY_TRACK
    }

    public void Play(MusicTracks track, float delay)
    {
        switch (track)
        {
            case MusicTracks.MENU_TRACK:
                if (gameplayTrack.IsPlaying())
                {
                    Stop(MusicTracks.GAMEPLAY_TRACK, true);
                }
                break;
            case MusicTracks.GAMEPLAY_TRACK:
                if (menuTrack.IsPlaying())
                {
                    Stop(MusicTracks.MENU_TRACK, true);
                }
                break;
        }
        StartCoroutine(DelayedPlay(track, delay));
    }
    IEnumerator DelayedPlay(MusicTracks track, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        
        switch (track)
        {
            case MusicTracks.MENU_TRACK:
                if (!menuTrack.IsPlaying())
                {
                    menuTrack.Play();
                }
                break;
            case MusicTracks.GAMEPLAY_TRACK:
                if (!gameplayTrack.IsPlaying())
                {
                    gameplayTrack.Play();
                }
                break;
        }
    }
    public void Stop(MusicTracks track, bool fadeOut)
    {
        switch (track)
        {
            case MusicTracks.MENU_TRACK:
                if (fadeOut)
                {
                    menuTrack.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
                else
                {
                    menuTrack.Stop();
                }
                break;
            case MusicTracks.GAMEPLAY_TRACK:
                if (fadeOut)
                {
                    gameplayTrack.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
                else
                {
                    gameplayTrack.Stop();
                }
                break;
        }
    }

    
    public void SetParameter(MusicParameters musicParameter, float value)
    {
        switch (musicParameter)
        {
            //case MusicParameters.EPICNESS:
            //    _epicness = value;
            //    break;
            case MusicParameters.CHARSELECT:
                _charSelect = value;
                break;
            case MusicParameters.SEGMENT:
                _segment = value;
                break;
            case MusicParameters.GLITCH:
                _glitch = value;
                break;
        }
        UpdateValues();
    }

    private void UpdateValues()
    {
        menuTrack.SetParameter("CharSelect", _charSelect);
        gameplayTrack.SetParameter("Segment", _segment);
        gameplayTrack.SetParameter("Glitch", _glitch);
    }

    public IEnumerator GlitchOut(float glitchAmount, float duration)
    {
        while (_glitch < glitchAmount)
        {
            _glitch += 0.05f;
            UpdateValues();
        }
        yield return new WaitForSeconds(duration);
        while (_glitch > 0)
        {
            _glitch -= 0.05f;
        }
        _glitch = 0f;
        UpdateValues();
    }
}
