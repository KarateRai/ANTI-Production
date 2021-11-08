using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    public Image abilityIcon;
    public Image abilityIconBG;
    public TMP_Text cooldownText;
    private float baseCooldown;
    private float currentTimer;
    private float turnOffTimer;
    private bool textOn;
    private void Update()
    {
        if (turnOffTimer > 0)
        {
            turnOffTimer -= Time.deltaTime;
        }
        else if (textOn)
        {
            cooldownText.alpha = 0;
            textOn = false;
        }
    }
    public void SetupIcon(float _cooldown, Sprite _icon)
    {
        abilityIcon.sprite = _icon;

    }
    private void SetCooldown()
    {
        float cdpercent = currentTimer / baseCooldown;
        int displayTime = (int)Math.Ceiling(currentTimer);
        cooldownText.text = displayTime.ToString();
        if (displayTime == 0 && textOn && turnOffTimer <= 0) 
        {
            turnOffTimer = 1;
        }
        else if (displayTime > 0)
        {
            cooldownText.alpha = 1;
            textOn = true;
        }
    }
    internal void UpdateLayout(float seconds)
    {
        currentTimer = seconds;
        SetCooldown();
    }
}
