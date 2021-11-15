using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class SoundEffectPlayer : MonoBehaviour
{
    private StudioEventEmitter emitter;
    private string filePath;

    private void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        filePath = emitter.Event;
    }

    public void PlaySound()
    {
        if (emitter != null)
        {
            RuntimeManager.PlayOneShotAttached(filePath, this.gameObject);
        }
    }
}
