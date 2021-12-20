using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public bool UseParticleCollision = false;
    private bool isAttacking = false;
    private float attackCountdown = 0;
    private float reloadContdown = 0;

    public Weapon equippedWeapon;
    [SerializeField] Transform shootingPosition;
    public bool useUsercolorProjectile;

    [SerializeField] LayerMask _targetLayer;

    public LayerMask TargetLayer => _targetLayer;

    #region Sight variables
    //Laser sight
    [SerializeField] private LineRenderer laserLine;
    private Vector3 laserTarget;
    private float dist2target;
    private float noTargetOffset = 1.5f;
    private float laserRange;
    private RaycastHit laserHit;
    private float xD, zD;
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
            laserTarget = shootingPosition.forward * noTargetOffset;
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

        if (Physics.Raycast(shootingPosition.position, shootingPosition.forward, out laserHit, laserRange, TargetLayer))
        {
            Debug.Log("HIT");
            CalculateDistance(laserHit);
        }
        else
            laserTarget = shootingPosition.transform.forward * noTargetOffset;
        laserLine.SetPosition(1, laserTarget);
    }

    private void CalculateDistance(RaycastHit hit)
    {
        
        xD = (shootingPosition.position.x - hit.point.x);
        zD = (shootingPosition.position.z - hit.point.z);
        dist2target = xD * xD + zD * zD;
        laserTarget = laserLine.transform.forward * dist2target;
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
