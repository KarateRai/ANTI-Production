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
    public Material[] materialList;
    public Material playerMaterial;
    public SkinnedMeshRenderer meshRenderer;

    private bool buildMode = false;

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

        AssignMeterial();
    }

    void AssignMeterial()
    {
        Material[] materialArray = meshRenderer.materials;


        switch (player.playerChoices.outfit)
        {
            case PlayerChoices.OutfitChoice.BLUE:
                playerMaterial = materialList[0];
                break;
            case PlayerChoices.OutfitChoice.GREEN:
                playerMaterial = materialList[1];
                break;
            case PlayerChoices.OutfitChoice.YELLOW:
                playerMaterial = materialList[2];
                break;
            case PlayerChoices.OutfitChoice.ORANGE:
                playerMaterial = materialList[3];
                break;
            case PlayerChoices.OutfitChoice.RED:
                playerMaterial = materialList[4];
                break;
            case PlayerChoices.OutfitChoice.PURPLE:
                playerMaterial = materialList[5];
                break;
        }
        materialArray[0] = playerMaterial;
        meshRenderer.materials = materialArray;
    }
    void FixedUpdate()
    {
        movement?.Update(input, aim);
    }

    private void InitializeCharacter()
    {
        movement = new Movement(this);
        stats = new PlayerStats(this, stats.Health, stats.Shield, stats.Speed);
        
    }

    public override void GainHealth(int amount)
    {
        stats.GainHealth(amount);
    }

    public override void TakeDamage(int amount)
    {
        stats.TakeDamage(amount);
        Debug.Log(stats.GetHPP());
    }

    public void UseAbilityOne(InputAction.CallbackContext context)
    {       
        if(context.performed)
        {
            unitAbilities.ActivateAbility(0);
        }
    }

    public void UseAbilityTwo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<UnitAbilities>().ActivateAbility(1);
        }
    }
    public void UseAbilityThree(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<UnitAbilities>().ActivateAbility(2);
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

    public void BuildMode(InputAction.CallbackContext context)
    {
        //Activate some build mode
    }

    public void Build(InputAction.CallbackContext context)
    {
        //Place chosen tower
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
    public IEnumerator DelayedResetSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.ResetSpeed();


    }

    public override IEnumerator Regen(int amountToRegen, float regenSpeed)
    {
        while (amountToRegen > 0 || Channeling == true)
        {
            stats.GainHealth(5);
            amountToRegen -= 5;
            yield return new WaitForSeconds(regenSpeed);
        }

    }
}
