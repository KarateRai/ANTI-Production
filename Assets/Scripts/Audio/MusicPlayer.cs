using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
[RequireComponent(typeof(StudioEventEmitter))]
public class MusicPlayer : MonoBehaviour
{

    private StudioEventEmitter emitter;

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
}
