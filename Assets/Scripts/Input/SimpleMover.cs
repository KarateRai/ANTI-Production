using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SimpleMover : MonoBehaviour
{
    CharacterController controller;
    public float moveSpeed = 6f;
    [Range(0, 1)]
    public float deadzone = 0.1f;
    private Vector3 direction;
    private Quaternion rotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (direction.magnitude > 0)
            controller.Move(direction * Time.deltaTime * moveSpeed);
        if (transform.rotation != rotation)
            transform.rotation = rotation;
    }

    public void DebugMessage(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log(context.action.name + " performed.");
        if (context.canceled)
            Debug.Log(context.action.name + " canceled.");
    }
    public void Movement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.magnitude >= deadzone)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            direction = Vector3.zero;
        }
    }
    public void Rotate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.magnitude >= deadzone)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }
}
