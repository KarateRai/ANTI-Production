using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSubMarker : MonoBehaviour
{
    public RectTransform markerTransform;
    public Image arrowImage;
    public TMP_Text nameText;
    public TMP_Text nameTextOOB;
    public CanvasGroup cGroup;
    public CanvasGroup defaultDisplayGroup;
    public CanvasGroup outOfBoundsDisplayGroup;
    private void Awake()
    {
        cGroup.alpha = 0;
    }
    public void SetValues(string _name, Color _color)
    {
        arrowImage.color = _color;
        nameText.color = _color;
        nameText.text = _name;
        nameTextOOB.color = _color;
        nameTextOOB.text = _name;
    }
    internal void Remove()
    {
        Destroy(this.gameObject);
    }
}
