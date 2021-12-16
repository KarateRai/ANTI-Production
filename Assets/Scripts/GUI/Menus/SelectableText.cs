using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectableText : MonoBehaviour
{
    public TMP_Text selectText;
    public bool disableColor;
    public void ChangeColor(Color newColor)
    {
        if (!disableColor)
            selectText.color = newColor;
    }
    
}
