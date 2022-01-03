using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public bool dynamicLaserSight = true;
    public bool UseParticleCollision = false;
    private bool isAttacking = false;
    private float attackCountdown = 0;
    private float reloadContdown = 0;
    private float secondaryAttackTimer = 0;

    public Weapon equippedWeapon;
    [SerializeField] Transform shootingPosition;
    public bool useUsercolorProjectile;
    [SerializeField] private SoundEffectPlayer soundEffect;

    [SerializeField] LayerMask _targetLayer;

    public LayerMask TargetLayer => _targetLayer;

    #region Sight variables
    //Laser sight
    [SerializeField] private LineRenderer laserLine;
    private float noTargetOffset = 1.5f;
    private float laserRange = 5f;
    private RaycastHit laserHit;
    #endregion

    public void SetShootingPos()
    {
        if (shootingPosition == null)
            shootingPosition = this.transform;
        if (UseParticleCollision == true)
            SetupParticleWeapon();
    }
   
    public void SetupRaycastWeapon(float range, Transform transform, Collider ownCollider, LayerMask targetLayer)
    {
        equippedWeapon.Init(range, transform, ownCollider, targetLayer);
    }
    

    public void SetupParticleWeapon()
    {
        if (useUsercolorProjectile == true)
        {
            equippedWeapon.Init(shootingPosition, TargetLayer);
            equippedWeapon.SetColor(GetComponent<PlayerController>().playerMaterial);
            //Laser sight
            laserLine.SetPosition(1, shootingPosition.forward * noTargetOffset);
        }
        else
            equippedWeapon.Init(shootingPosition, TargetLayer);
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
        UpdateLaserSight();

        if (isAttacking == true)
            Fire();
    }

    private void Countdown()
    {
        if (equippedWeapon.AbilityAvaiable)
        {
            if (attackCountdown > 0)
            {
                attackCountdown -= Time.deltaTime;
            }
            else if (attackCountdown <= 0 && isAttacking == false)
            {
                if (secondaryAttackTimer < equippedWeapon.ChargeTime)
                    secondaryAttackTimer += Time.deltaTime;
                else
                    secondaryAttackTimer = equippedWeapon.ChargeTime;
            }
            if (isAttacking == true && secondaryAttackTimer > 0 && equippedWeapon.Ability.Activated == true)
                secondaryAttackTimer -= Time.deltaTime;
        }
        else
        {
            if (attackCountdown > 0)
            {
                attackCountdown -= Time.deltaTime;
            }
        }
        

        if (reloadContdown > 0)
        {
            reloadContdown -= Time.deltaTime;
        }
    }

    private void UpdateLaserSight()
    {
        if (laserLine == null)
            return;

        laserLine.SetPosition(0, shootingPosition.position);
        
        if (dynamicLaserSight != true)
        {
            laserLine.SetPosition(1, shootingPosition.position + shootingPosition.forward * laserRange);
        }
        else
        {
            if (Physics.Raycast(shootingPosition.position, shootingPosition.forward, out laserHit, laserRange, TargetLayer))
            {
                laserLine.SetPosition(1, shootingPosition.position + shootingPosition.forward * Vector3.Distance(shootingPosition.position, laserHit.point));
            }
            else
                laserLine.SetPosition(1, shootingPosition.position + shootingPosition.forward * noTargetOffset);
        }


    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
        }
        if (context.canceled)
        {
            isAttacking = false;
        }
    }
    public void Fire()
    {
        if (attackCountdown > 0)
            return;

        if (equippedWeapon.AbilityAvaiable)
        {
            if (secondaryAttackTimer >= equippedWeapon.ChargeTime && equippedWeapon.Ability.Activated == false)
            {
                equippedWeapon.ActivateAbility(equippedWeapon);
                secondaryAttackTimer = equippedWeapon.PowerTime;
            }
            else if (reloadContdown <= 0 && secondaryAttackTimer <= 0 || equippedWeapon.Ability.ChargesLeft == 0)
            {
                equippedWeapon.DeactivateAbility(equippedWeapon);
                secondaryAttackTimer = 0;
            }
        }
        
        
        if (soundEffect)
            soundEffect.PlaySound();

        if (!equippedWeapon.Fire())
        {
            //Reloading
            reloadContdown = equippedWeapon.ReloadTime;
        }

        attackCountdown = 1f / equippedWeapon.Firerate;
    }

    public void Fire(GameObject target)
    {
        //Homing fire
        if (attackCountdown <= 0 && reloadContdown <= 0)
        {
            if (!equippedWeapon.Fire(target))
            {
                //Reloading
                reloadContdown = equippedWeapon.ReloadTime;
            }

            attackCountdown = 1f / equippedWeapon.Firerate;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(shootingPosition.position, shootingPosition.forward, Color.blue);
    }
}
