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
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
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
        if (attackCountdown <= 0 && reloadContdown <= 0)
        {
            if (soundEffect)
                soundEffect.PlaySound();

            if (!equippedWeapon.Fire())
            {
                //Reloading
                reloadContdown = equippedWeapon.ReloadTime;
            }
            
            //Old projectile fire, left if needed

            //GameObject bullet = BulletObjectPool.SharedInstance.GetPooledBullet();
            //if (bullet != null)
            //{
            //    bullet.transform.position = transform.position;
            //    bullet.transform.rotation = transform.rotation;
            //    bullet.SetActive(true);
            //}
            //else
            //{
                  //Reload
            //    Debug.LogError("Out of ammo");
            //}
            
            attackCountdown = 1f / equippedWeapon.Firerate;
        }
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
