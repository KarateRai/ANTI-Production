using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController
{
    private Movement movement;
    private Vector2 input = Vector2.zero;
    private Vector2 aim = Vector2.zero;

    ///---------------Character variables---------------///

    public PlayerStats stats;
    public UnitAbilities unitAbilities;
    public UnitRole role;

    public Player player;


    void Start()
    {
        InitializeCharacter();
        movement.animator = GetComponent<Animator>();
    }
    public void AssignPlayer(int playerID)
    {
        player = PlayerManager.instance.players[playerID];
        role = PlayerManager.instance.GetPlayerRole(player.playerChoices.role);
        
        unitAbilities.AddCooldowns(this);
    }


    void FixedUpdate()
    {
        movement.Update(input, aim);
    }

    private void InitializeCharacter()
    {
        movement = new Movement(this);
        stats = new PlayerStats(this, stats.Health, stats.Speed);
    }

    public override void GainHealth(int amount)
    {
        stats.GainHealth(amount);
    }

    public override void TakeDamage(int amount)
    {
        stats.TakeDamage(amount);
    }

    public void UseAbilityOne(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            Debug.Log("ability one!");
            //GUIManager.instance.messageToast.NewMessage("ability one!");
            unitAbilities.ActivateAbility(0);
            //Debug.Log("Starting Wave!");
            //FindObjectOfType<WaveSpawner>().StartWaves();
        }

    }

    public void UseAbilityTwo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<UnitAbilities>().ActivateAbility(1);
            Debug.Log("ability two!");
            //GUIManager.instance.messageToast.NewMessage("ability two!");
            //FindObjectOfType<WaveSpawner>().KillWave();
        }
    }

    public void UseWeapon(InputAction.CallbackContext context)
    {
        weaponController.Fire();
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

    public override void AffectSpeed(int amount)
    {
        stats.SetSpeed(amount);
    }
}
