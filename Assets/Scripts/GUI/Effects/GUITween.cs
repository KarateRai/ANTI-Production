using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUITween : MonoBehaviour
{
    public UnityEvent onEnableStarted, onEnableComplete, onDisableStarted, onDisableComplete;
    private bool isOn, isTweening;
    public enum AnimationType
    {
        SCALE,
        MOVE
    }
    [SerializeField] AnimationType[] enableAnimations, disableAnimations;
    [SerializeField] Vector2 onPosition = new Vector2(0, 0), offPosition, onScale = new Vector2(1, 1), offScale;
    [SerializeField] float enableDuration = 0.5f, disableDuration = 0.5f;
    private void Start()
    {
        Disable();
    }
    public bool TweenActive()
    {
        return isTweening;
    }
    public void Enable()
    {

        OnStarted(1);
        //SetToOffValues();
        gameObject.SetActive(true);
        for (int i = 0; i < enableAnimations.Length; i++)
        {
            EnableWith(enableAnimations[i]);
        }
        
    }
    private void SetToOffValues()
    {
        bool useDefaults = true;
        for (int i = 0; i < disableAnimations.Length; i++)
        {
            if (disableAnimations[i] == AnimationType.MOVE)
            {
                useDefaults = false;
            }
        }
        if (!useDefaults)
        {
            transform.localScale = new Vector3(offScale.x, offScale.y, 0);
            transform.localPosition = new Vector3(offPosition.x, offPosition.y, 0);
        }
    }

    private void EnableWith(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.SCALE:
                LeanTween.scale(gameObject, new Vector3(onScale.x, onScale.y, 1), enableDuration)
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() => OnComplete(1));
                break;
            case AnimationType.MOVE:
                LeanTween.moveLocal(gameObject, new Vector3(onPosition.x, onPosition.y, 0), enableDuration)
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() => OnComplete(1));
                break;
        }
    }
    public void Disable()
    {

        OnStarted(0);
        for (int i = 0; i < enableAnimations.Length; i++)
        {
            DisableWith(disableAnimations[i]);
        }
    }
    
    private void DisableWith(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.SCALE:
                LeanTween.scale(gameObject, new Vector3(offScale.x, offScale.y, 0), disableDuration)
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() => OnComplete(0));
                break;
            case AnimationType.MOVE:
                LeanTween.moveLocal(gameObject, new Vector3(offPosition.x, offPosition.y, 0), disableDuration)
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() => OnComplete(0));
                break;
        }
    }
    private void OnStarted(int onoff)
    {
        isTweening = true;
        switch (onoff)
        {
            case 0:
                onDisableStarted?.Invoke();
                break;
            case 1:
                onEnableStarted?.Invoke();
                break;
            default:
                break;
        }
    }
    private void OnComplete(int onoff)
    {
        isTweening = false;
        switch (onoff)
        {
            case 0:
                if (isOn)
                {
                    isOn = false;
                    onDisableComplete?.Invoke();
                    gameObject.SetActive(false);
                }
                break;
            case 1:
                if (!isOn)
                {
                    isOn = true;
                    onEnableComplete?.Invoke();
                }
                break;
            default:
                break;
        }
    }
}
