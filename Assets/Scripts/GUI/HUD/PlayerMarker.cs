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
    //private void Update()
    //{
    //    pos = new Vector2(clampGUI.GetPos().x, clampGUI.GetPos().y) + offset;
    //}
    private void FixedUpdate()
    {
        subMarker.markerTransform.LeanSetPosX(clampGUI.GetPos().x);
        subMarker.markerTransform.LeanSetPosY(clampGUI.GetPos().y + offsetY);
    }
}
