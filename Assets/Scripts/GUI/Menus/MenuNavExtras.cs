using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuNavExtras : MonoBehaviour
{
    [HideInInspector]
    public GameObject selected;
    private bool resetStickNav = true;
    private float stickResetTimer;
    private float stickResetWait = 0.3f;
    public void ChangeSelected(GameObject toSelect)
    {
        selected = toSelect;
    }
    private void Update()
    {
        if (resetStickNav) { stickResetTimer = 0; }
        if (!resetStickNav && stickResetTimer > 0)
        {
            stickResetTimer -= Time.deltaTime;
            if (stickResetTimer <= 0)
            {
                stickResetTimer = 0;
                resetStickNav = true;
            }
        }
    }
    public void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        value.y = 0;
        if (context.performed && value.magnitude > 0.1 && resetStickNav)
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
        else if (value.magnitude < 0.1)
        {
            resetStickNav = true;
        }
    }
    protected virtual void OnNavLeft() { }
    protected virtual void OnNavRight() { }
}
