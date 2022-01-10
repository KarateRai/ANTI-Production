using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class SoundEffectPlayer : MonoBehaviour
{
    private StudioEventEmitter emitter;
    private string filePath;
    private bool playOnStart;
    private void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        filePath = emitter.Event;
        if (emitter.PlayEvent == EmitterGameEvent.ObjectStart) { playOnStart = true; }
    }

    private void Update()
    {
        if (emitter.IsPlaying() && Time.timeScale == 0)
        {
            emitter.Stop();
        }
        else if (!emitter.IsPlaying() && Time.timeScale > 0 && playOnStart)
        {
            PlaySound();
        }
    }
    public void PlaySound()
    {
        if (emitter != null)
        {
            RuntimeManager.PlayOneShotAttached(filePath, this.gameObject);
        }
    }
}
