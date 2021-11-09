using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController
{

    /// TEST
    bool testBool = false;


    private Movement movement;
    private Vector2 input = Vector2.zero;
    private Vector2 aim = Vector2.zero;

    ///---------------Character variables---------------///

    public PlayerStats stats;


    // Start is called before the first frame update
    void Start()
    {
        InitializeCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (testBool == false)
        {
            Debug.Log("Testing some stats: " + stats.Health);
            TakeDamage(20);
            testBool = true;
        }
        movement.Update(input, aim);
    }

    private void InitializeCharacter()
    {
        movement = new Movement(this);
        stats = new PlayerStats(this);
    }

    public override void GainHealth(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int amount)
    {
        Debug.Log("Health before: " + stats.Health);
        stats.TakeDamage(amount);
        Debug.Log("Health after: " + stats.Health);
    }

    public void UseAbilityOne(InputAction.CallbackContext context)
    {
        Debug.Log("Starting Wave!");
        FindObjectOfType<WaveSpawner>().StartWaves();
    }

    public void UseAbilityTwo(InputAction.CallbackContext context)
    {
        Debug.Log("Killing Wave!");
        FindObjectOfType<WaveSpawner>().KillWave();
    }

    public void UseWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking!");
    }

    public void Character_Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void Character_Aim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    public void Spawn()
    {
        //Move to starting position
        gameObject.SetActive(true);
        isDead = false;
    }
}
