using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : UnitController
{
    private Movement movement;
    private Vector2 input, aim;

    ///---------------Character variables---------------///

    public PlayerStats stats;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.Update(input, aim);
    }

    public override void GainHealth(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void UseWeapon(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void Character_Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void Character_Aim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
