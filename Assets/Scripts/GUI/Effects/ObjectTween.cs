using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTween : MonoBehaviour
{
    public UnityAction onEnableStarted, onEnableComplete, onDisableStarted, onDisableComplete;
    public UnityEvent onEnableEventStart, onEnableEventEnd, onDisableEventStart, onDisableEventEnd;
    [SerializeField] Vector3 enableMovement, disableMovement;
    public Vector3 startPosition, enabledPosition, disabledPosition;
    public float enableDuration = 0.5f, disableDuration = 0.5f;
    private void Start()
    {
        SetPosValues();
        Enable();
    }
    private void SetPosValues()
    {
        startPosition.x = transform.localPosition.x;
        startPosition.y = transform.localPosition.y;
        startPosition.z = transform.localPosition.z;
        enabledPosition.x = startPosition.x + enableMovement.x;
        enabledPosition.y = startPosition.y + enableMovement.y;
        enabledPosition.z = startPosition.z + enableMovement.z;
        disabledPosition.x = enabledPosition.x + disableMovement.x;
        disabledPosition.y = enabledPosition.y + disableMovement.y;
        disabledPosition.z = enabledPosition.z + disableMovement.z;
    }
    public void Enable()
    {
        OnStarted(1);
        LeanTween.move(gameObject, enabledPosition, enableDuration)
                    .setIgnoreTimeScale(false)
                    .setOnComplete(() => OnComplete(1));
    }
    
    public void Disable()
    {
        OnStarted(0);
        LeanTween.move(gameObject, new Vector3(disabledPosition.x, disabledPosition.y, disabledPosition.z), disableDuration)
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() => OnComplete(0));
    }
    

    private void OnStarted(int onoff)
    {
        switch (onoff)
        {
            case 0:
                onDisableStarted?.Invoke();
                onDisableEventStart?.Invoke();
                break;
            case 1:
                onEnableStarted?.Invoke();
                onEnableEventStart?.Invoke();
                break;
            default:
                break;
        }
    }
    private void OnComplete(int onoff)
    {
        switch (onoff)
        {
            case 0:
                onDisableComplete?.Invoke();
                onDisableEventEnd?.Invoke();
                break;
            case 1:
                onEnableComplete?.Invoke();
                onEnableEventEnd?.Invoke();
                break;
            default:
                break;
        }
    }
}
