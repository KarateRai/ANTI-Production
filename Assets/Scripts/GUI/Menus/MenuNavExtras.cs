using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuNavExtras : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnNavLeftOrRight;
    protected UnityAction selectedChanged;
    //[HideInInspector]
    public GameObject selected;
    public GameObject[] selectables; 
    protected bool resetStickNav = true;
    protected float stickResetTimer;
    protected float stickResetWait = 0.2f;
    protected float stickResetSpeed = 0.017f;
    public void ChangeSelected(GameObject toSelect)
    {
        selected = toSelect;
        selectedChanged?.Invoke();
        SetColors();
    }
    public void OnDeselect(GameObject deselected)
    {
        if (deselected.GetComponent<SelectableText>() != null)
            deselected.GetComponent<SelectableText>().ChangeColor(GUIManager.instance.defaultColor);
    }
    private void Update()
    {
        if (resetStickNav) { stickResetTimer = 0; }
        if (!resetStickNav && stickResetTimer > 0)
        {
            stickResetTimer -= Time.unscaledDeltaTime;
            //Debug.Log("Stick Timer: " + stickResetTimer);
            if (stickResetTimer <= 0)
            {
                stickResetTimer = 0;
                resetStickNav = true;
            }
        }
        ExtraUpdate();
    }
    public void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        value.y = 0;
        if (context.performed && value.magnitude > 0.4 && resetStickNav)
        {
            resetStickNav = false;
            stickResetTimer = stickResetWait;
            if (value.x < 0)
            {
                OnNavLeft();
            }
            else if (value.x > 0)
            {
                OnNavRight();
            }
        }
        //else if (context.canceled)
        //{
        //    resetStickNav = true;
        //}
    }
    protected void SetColors()
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            if (selectables[i] == selected && selectables[i].GetComponent<SelectableText>() != null)
                selectables[i].GetComponent<SelectableText>().ChangeColor(GUIManager.instance.selectedColor);
            else if (selectables[i].GetComponent<SelectableText>() != null)
                selectables[i].GetComponent<SelectableText>().ChangeColor(GUIManager.instance.defaultColor);
        }
    }
    protected virtual void OnNavLeft() { }
    protected virtual void OnNavRight() { }
    protected virtual void ExtraUpdate() { }
}
