using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    private PlayerController controller;
    private Transform cam;
    private Rigidbody rb;

    ///--------------------Animation----------------------------///
    public Animator animator;

    public Movement(PlayerController controller)
    {
        this.controller = controller;
        this.rb = controller.GetComponent<Rigidbody>();
        this.cam = Camera.main.transform;
    }

    // Update is called once per frame
    public void Update(Vector2 input, Vector2 aim)
    {
        //Update movement here
        if (aim.magnitude > 0.1)
        {
            float aimAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Turn(aimAngle);
        }
        if (input.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 adjustedDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity = adjustedDirection * controller.stats.Speed;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        ///--------------------Animation----------------------------///
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);
        ///---------------------------------------------------------///
    }
    private void Turn(float angle)
    {
        controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

}
