using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

[RequireComponent(typeof(StudioEventEmitter))]
public class MusicPlayer : MonoBehaviour
{

    private StudioEventEmitter emitter;
    [Range(0, 100f)]
    private float _epicness = 0;
    public enum MusicParameters
    {
        EPICNESS
    }

    private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }

    public void Play()
    {
        if (!emitter.IsPlaying())
        {
            emitter.Play();
        }
    }

    public void Stop()
    {
        emitter.Stop();
    }

    public void SetParameter(MusicParameters musicParameter, float value)
    {
        switch (musicParameter)
        {
            case MusicParameters.EPICNESS:
                _epicness = value;
                break;
        }
        UpdateValues();
    }

    private void UpdateValues()
    {
        emitter.SetParameter("Epicness", _epicness);
    }
}
