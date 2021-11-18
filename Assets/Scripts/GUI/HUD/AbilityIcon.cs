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
    public float currentTimer;
    private float turnOffTimer = 0;
    public bool textOn = true;
    public int displayTime;
    public float cdpercent = 1;
    private void Update()
    {
        if (turnOffTimer > 0 && currentTimer <= 0)
        {
            turnOffTimer -= Time.deltaTime;
            textOn = true;
        }
        else if (turnOffTimer > 0)
        {
            textOn = true;
        }
        else if (textOn)
        {
            textOn = false;
        }

        if (textOn)
        {
            cooldownText.alpha = 255;
        }
        else
        {
            cooldownText.alpha = 0;
        }
    }
    public void SetupIcon(float _cooldown, Sprite _icon)
    {
        abilityIcon.sprite = _icon;
        baseCooldown = _cooldown;
    }
    private void SetCooldown()
    {
        cdpercent = (baseCooldown-currentTimer) / baseCooldown;
        abilityIcon.fillAmount = cdpercent;
        displayTime = (int)Math.Ceiling(currentTimer);
        cooldownText.text = displayTime.ToString();
        if (displayTime > 0) 
        {
            turnOffTimer = 1;
        }
        //else if (displayTime > 0)
        //{
        //    textOn = true;
        //}
    }
    internal void UpdateLayout(float seconds)
    {
        currentTimer = seconds;
        SetCooldown();
    }
}
