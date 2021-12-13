using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubBar : MonoBehaviour
{
    public RectTransform barTransform;
    public SmoothFillBar healthFillBar;
    public SmoothFillBar armorFillBar;
    public CanvasGroup defaultDisplayGroup;
    public CanvasGroup outOfBoundsDisplayGroup;
    internal void Remove()
    {
        Destroy(this.gameObject);
    }
}
