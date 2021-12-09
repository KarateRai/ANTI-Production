using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    public GameObject markerPrefab;
    public ClampGUIToTransform clampGUI;
    public float offsetY;
    [HideInInspector]
    public PlayerSubMarker subMarker;
    private Vector2 pos;
    private bool isVisible;
    private void Awake()
    {
        subMarker = Instantiate(markerPrefab, GUIManager.instance.playerHUD.trackedObjects.transform).GetComponent<PlayerSubMarker>();
        pos = Vector2.zero;
    }
    private void OnDestroy()
    {
        if (subMarker != null)
            subMarker.Remove();
    }
    public void Toggle(bool turnOn)
    {
        isVisible = turnOn;
        switch (turnOn)
        {
            case true:
                subMarker.cGroup.alpha = 1;
                break;
            case false:
                subMarker.cGroup.alpha = 0;
                break;
        }
    }
    private void Update()
    {
        pos = new Vector2(clampGUI.GetPos().x, clampGUI.GetPos().y);
        pos.y += offsetY;
    }
    private void FixedUpdate()
    {
        int xMax = 1883;
        int xMin = 37;
        int yMax = 1043;
        int yMin = 37;
        bool outOfBounds = false;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax) { outOfBounds = true; }

        if (!outOfBounds)
        {
            subMarker.defaultDisplayGroup.alpha = 1;
            subMarker.outOfBoundsDisplayGroup.alpha = 0;
        }
        else
        {
            subMarker.defaultDisplayGroup.alpha = 0;
            subMarker.outOfBoundsDisplayGroup.alpha = 1;
            if (pos.x < xMin) { pos.x = xMin; }
            else if (pos.x > xMax) { pos.x = xMax; }
            if (pos.y < yMin) { pos.y = yMin; }
            else if (pos.y > yMax) { pos.y = yMax; }
        }
        subMarker.markerTransform.LeanSetPosX(pos.x);
        subMarker.markerTransform.LeanSetPosY(pos.y);

        //subMarker.markerTransform.LeanSetPosX(clampGUI.GetPos().x);
        //subMarker.markerTransform.LeanSetPosY(clampGUI.GetPos().y + offsetY);
    }
}
