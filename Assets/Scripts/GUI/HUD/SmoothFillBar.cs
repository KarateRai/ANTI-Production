using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothFillBar : MonoBehaviour
{
    public Image FillBar;
    public Color HighValue = Color.green;
    public Color MidValue = Color.yellow;
    public Color LowValue = Color.red;
    public bool useColors;
    public int MaxValue = 100;
    [Range(0, 100)]
    public int CurrentValue = 0;
    public bool SmoothFill = true;
    private int SmoothHealth = 100;
    private float colorMod;
    public float currentPercentage = 0f;
    
    private void Update()
    {
        AdjustSmoothValue();
        DisplayFill();
        ChangeColor();
    }
    public void UpdateValues(int percentValue)
    {
        CurrentValue = percentValue;
    }

    private void AdjustSmoothValue()
    {
        if (SmoothFill)
        {
            if (SmoothHealth < CurrentValue)
            {
                SmoothHealth += 1;
            }
            else if (SmoothHealth > CurrentValue)
            {
                SmoothHealth -= 1;
            }
        }
        else
        {
            SmoothHealth = CurrentValue;
        }
        currentPercentage = (float)SmoothHealth / 100f;
    }

    private void DisplayFill()
    {
        FillBar.fillAmount = currentPercentage;
    }

    private void ChangeColor()
    {
        if (useColors)
        {
            if (SmoothHealth <= 50)
            {
                colorMod = -0.25f + SmoothHealth / 50f * 1.5f;
                FillBar.color = Color.Lerp(LowValue, MidValue, colorMod);
            }
            else
            {
                colorMod = -0.25f + (SmoothHealth - 50f) / 50f * 1.5f;
                FillBar.color = Color.Lerp(MidValue, HighValue, colorMod);
            }
        }
    }

    internal void SetImmediateValues(int percentage)
    {
        CurrentValue = percentage;
        SmoothHealth = percentage;
    }
}
