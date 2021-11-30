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

    void Start()
    {
        equippedWeapon = Object.Instantiate(equippedWeapon);

        if (shootingPosition == null)
            shootingPosition = this.transform;
        if (UseParticleCollision == true)
            SetupParticleWeapon();
    }

    public void SetupRaycastWeapon(float range, Transform transform, Collider ownCollider, LayerMask targetLayer)
    {
        equippedWeapon.Init(range, transform, ownCollider, targetLayer);
    }

    private void SetupParticleWeapon()
    {
        if (useUsercolorProjectile == true)
        {
            //Send in color to weapon here
            //equippedWeapon.Init(shootingPosition, TargetLayer, );
        }
        else
            equippedWeapon.Init(shootingPosition, TargetLayer);
    }

    // Update is called once per frame
    void Update()
    {
        #region Countdowns
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;    
        }
        if (reloadContdown > 0)
        {
            reloadContdown -= Time.deltaTime;
        }
        #endregion
        if (isAttacking == true)
        {
            Fire();
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
            Debug.Log("Using weapon");
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
            //    Debug.LogError("Out of ammo");
            //}
            
            attackCountdown = 1f / equippedWeapon.Firerate;
        }
    }
}
