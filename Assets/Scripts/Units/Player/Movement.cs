using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    private PlayerController controller;
    private Transform cam;
    private Rigidbody rb;
    public float tAngle;
    private Vector3 _movement;

    ///--------------------Animation----------------------------///
    public Animator animator;

    public Movement(PlayerController controller)
    {
        this.controller = controller;
        this.rb = controller.GetComponent<Rigidbody>();
        this.cam = Camera.main.transform;
        this._movement = Vector3.zero;
    }

    // Update is called once per frame
    public void Update(Vector2 input, Vector2 aim)
    {
        
        //Update movement here
        if (aim.magnitude > 0.1)
        {
            float aimAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            tAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Turn(aimAngle);
        }
        if (input.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 adjustedDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _movement = adjustedDirection * input.magnitude * controller.stats.Speed;
            if (rb.velocity.y <= 0)
            {
                _movement.y = rb.velocity.y;
            }
            //Ignore y if it is positive?
            
            rb.velocity = _movement;
        }
        else
        {
            _movement = Vector3.zero;
            _movement.y = rb.velocity.y;
            rb.velocity = _movement;
        }

        ///--------------------Animation----------------------------///
        //float tAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg + cam.eulerAngles.y;

        //float angle = Vector2.Angle(new Vector2(1, 0), input);
        //float tAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float newX = (input.x * Mathf.Cos(Mathf.Deg2Rad * tAngle)) - (input.y * Mathf.Sin(Mathf.Deg2Rad * tAngle));
        float newY = (input.y * Mathf.Cos(Mathf.Deg2Rad * tAngle)) + (input.x * Mathf.Sin(Mathf.Deg2Rad * tAngle));
        animator.SetFloat("X", newX);
        animator.SetFloat("Y", newY);
        

        ///---------------------------------------------------------///
    }
    private void Turn(float angle)
    {
        controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

}
