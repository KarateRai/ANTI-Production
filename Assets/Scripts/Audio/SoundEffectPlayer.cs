using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class SoundEffectPlayer : MonoBehaviour
{
    private StudioEventEmitter emitter;
    private string filePath;
    public bool playOnStart;
    //private bool playOnStart;
    private void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        filePath = emitter.Event;
        //if (emitter.PlayEvent == EmitterGameEvent.ObjectStart) { playOnStart = true; }
        if (playOnStart)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (emitter != null)
        {
            emitter.Play();
            //RuntimeManager.PlayOneShotAttached(filePath, this.gameObject);
        }
    }
}
