using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubBar : MonoBehaviour
{
    public RectTransform barTransform;
    public SmoothFillBar healthFillBar;
    public SmoothFillBar armorFillBar;
    internal void Remove()
    {
        Destroy(this.gameObject);
    }
}
