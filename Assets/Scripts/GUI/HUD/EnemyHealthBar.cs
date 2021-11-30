using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public RectTransform barTransform;
    public SmoothFillBar healthFillBar;
    public SmoothFillBar armorFillBar;
    public ClampGUIToTransform clampGUI;
    private void Update()
    {
        barTransform.LeanSetPosX(clampGUI.GetPos().x);
        barTransform.LeanSetPosY(clampGUI.GetPos().y);
    }
    public void UpdateHealth(int percentage)
    {
        healthFillBar.UpdateValues(percentage);
    }
    public void UpdateArmor(int percentage)
    {
        armorFillBar.UpdateValues(percentage);
    }
}
