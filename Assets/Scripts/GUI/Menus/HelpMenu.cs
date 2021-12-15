using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelpMenu : MenuNavExtras
{
    public GameObject[] helpPages;
    public SoundEffectPlayer effectPlayer;
    int activeIndex = 0;
    public TMP_Text pageCountText;
    bool isTweening;
    public float flipTime = 0.3f;
    public float flipDistance = 2000f;
    private void Start()
    {
        selectedChanged += RefreshPageCount;
        InitiatePages();
    }
    private void OnDestroy()
    {
        selectedChanged -= RefreshPageCount;
    }
    private void InitiatePages()
    {
        for (int i = 0; i < helpPages.Length; i++)
        {
            if (i != 0)
            {
                LeanTween.moveLocalX(helpPages[i], flipDistance, 0f);
            }
            else
            {
                LeanTween.moveLocalX(helpPages[i], 0, 0f);
            }
        }
    }
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToStart();
        }
    }
    private void RefreshPageCount()
    {
        pageCountText.text = activeIndex + 1 + "/" + helpPages.Length;
    }
    public void ReturnToStart()
    {
        StartCoroutine(ResetPages());
        GUIManager.instance.CloseMenu("HELP_MENU");
        GUIManager.instance.OpenMenu("START_MENU");
    }

    IEnumerator ResetPages()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        InitiatePages();
        activeIndex = 0;
        RefreshPageCount();
    }

    protected override void OnNavLeft()
    {
        switch (selected.name)
        {
            case "ArrowButtonPageSelect":
                if (!isTweening)
                    FlipPages(-1);
                break;
        }
    }
    protected override void OnNavRight()
    {
        switch (selected.name)
        {
            case "ArrowButtonPageSelect":
                if (!isTweening)
                    FlipPages(1);
                break;
        }
    }

    private void FlipPages(int i)
    {
        if (i == 1 && activeIndex < helpPages.Length-1)
        {
            isTweening = true;
            GameObject oldPage = helpPages[activeIndex];
            GameObject newPage = helpPages[activeIndex+1];
            activeIndex += 1;
            MoveFromPage(oldPage, newPage, 1);
        }
        else if (i == -1 && activeIndex > 0)
        {
            isTweening = true;
            GameObject oldPage = helpPages[activeIndex];
            GameObject newPage = helpPages[activeIndex - 1];
            activeIndex -= 1;
            MoveFromPage(oldPage, newPage, -1);
        }
    }

    private void MoveFromPage(GameObject fromPage,GameObject toPage, int direction)
    {
        effectPlayer.PlaySound();
        LeanTween.moveLocalX(fromPage, -direction * flipDistance, flipTime)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => MoveToPage(toPage));
    }
    private void MoveToPage(GameObject page)
    {
        RefreshPageCount();
        effectPlayer.PlaySound();
        LeanTween.moveLocalX(page, 0, flipTime)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => isTweening = false);
    }
}
