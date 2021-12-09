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
    public CanvasGroup cGroup;
    private void Awake()
    {
        cGroup.alpha = 0;
    }
    public void SetValues(string _name, Color _color)
    {
        arrowImage.color = _color;
        nameText.color = _color;
        nameText.text = _name;
    }
    internal void Remove()
    {
        Destroy(this.gameObject);
    }
}
