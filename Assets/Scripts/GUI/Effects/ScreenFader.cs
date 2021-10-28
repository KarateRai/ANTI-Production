using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public UnityEvent onFadeOutComplete, onFadeInComplete, onFadeInStarted, onFadeOutStarted;
    public enum FadeState
    {
        ON_BLACK,
        FADING_IN,
        ON_TRANSPARENT,
        FADING_OUT
    }
    public FadeState fadeState;
    private void Start()
    {
        fadeCanvasGroup.alpha = 1;
    }
    public void FadeOut(float duration)
    {
        fadeState = FadeState.FADING_OUT;
        onFadeOutStarted.Invoke();
        LeanTween.alphaCanvas(fadeCanvasGroup, 1, duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => FadeOutComplete());
    }
    public void FadeIn(float duration)
    {
        fadeState = FadeState.FADING_IN;
        onFadeInStarted.Invoke();
        LeanTween.alphaCanvas(fadeCanvasGroup, 0, duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(()=> FadeInComplete());
    }

    public void FadeInComplete()
    {
        fadeState = FadeState.ON_TRANSPARENT;
        onFadeInComplete.Invoke();
        //Debug.Log("FadeInComplete");
    }
    public void FadeOutComplete()
    {
        fadeState = FadeState.ON_BLACK;
        onFadeOutComplete.Invoke();
        //Debug.Log("FadeOutComplete");
    }
}
