using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MessageToast : MonoBehaviour
{
    public GUITween leftBarTween;
    public GUITween rightBarTween;
    public GUITween textTween;
    public TMP_Text toastText;
    public List<Toast> toastQueue;
    private bool isToasting;
    private float nextDuration;
    private bool pauseToasts;
    private void Start()
    {
        ClearToastQueue();
        leftBarTween.onDisableComplete += ToastDone;
        textTween.onEnableComplete += WaitToClose;
        textTween.onDisableComplete += leftBarTween.Disable;
        textTween.onDisableComplete += rightBarTween.Disable;
        leftBarTween.onEnableComplete += textTween.Enable;
        GlobalEvents.instance.onGamePaused += PauseOn;
        GlobalEvents.instance.onGameUnpaused += PauseOff;
        GlobalEvents.instance.onGameOver += ClearToastQueue;
        GlobalEvents.instance.onStageSceneEnd += ClearToastQueue;
    }
    private void OnDestroy()
    {
        leftBarTween.onDisableComplete -= ToastDone;
        textTween.onEnableComplete -= WaitToClose;
        textTween.onDisableComplete -= leftBarTween.Disable;
        textTween.onDisableComplete -= rightBarTween.Disable;
        leftBarTween.onEnableComplete -= textTween.Enable;
        GlobalEvents.instance.onGamePaused -= PauseOn;
        GlobalEvents.instance.onGameUnpaused -= PauseOff;
        GlobalEvents.instance.onGameOver -= ClearToastQueue;
        GlobalEvents.instance.onStageSceneEnd += ClearToastQueue;
    }
    private void Update()
    {
        if (toastQueue.Count > 0 && !isToasting && !pauseToasts)
        {
            NextToast();
        }
    }
    private void PauseOn()
    {
        pauseToasts = true;
    }
    private void PauseOff()
    {
        pauseToasts = false;
    }
    public void NewMessage(string message, float duration)
    {
        Toast newToast = new Toast(message, duration);
        toastQueue.Add(newToast);
    }
    public void NewMessage(string message)
    {
        if (toastQueue.Count < 10)
        {
            Toast newToast = new Toast(message, 2);
            toastQueue.Add(newToast);
        }
    }
    private void NextToast()
    {
        isToasting = true;
        toastText.text = toastQueue[0].message;
        nextDuration = toastQueue[0].duration;
        toastQueue.RemoveAt(0);
        leftBarTween.Enable();
        rightBarTween.Enable();
    }    
    private void WaitToClose()
    {
        StartCoroutine(DelayedClose());
    }
    IEnumerator DelayedClose()
    {
        yield return new WaitForSecondsRealtime(nextDuration);
        textTween.Disable();
    }
    private void ToastDone()
    {
        isToasting = false;
    }
    public void ClearToastQueue()
    {
        toastQueue = new List<Toast>();
    }
}
