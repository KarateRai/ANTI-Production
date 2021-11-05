using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuNavExtras : MonoBehaviour
{
    [HideInInspector]
    public GameObject selected;
    private bool resetStickNav = true;
    public void ChangeSelected(GameObject toSelect)
    {
        selected = toSelect;
    }
    public void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        value.y = 0;
        if (context.performed && value.magnitude > 0.3 && resetStickNav)
        {
            resetStickNav = false;
            if (value.x < 0)
            {
                OnNavLeft();
            }
            else if (value.x > 0)
            {
                OnNavRight();
            }
        }
        else if (value.magnitude < 0.3)
        {
            resetStickNav = true;
        }
    }
    protected virtual void OnNavLeft() { }
    protected virtual void OnNavRight() { }
}
