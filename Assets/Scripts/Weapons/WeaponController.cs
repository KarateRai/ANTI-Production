using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private bool isAttacking = false;
    private float attackCountdown = 0;
    private float reloadContdown = 0;

    public Weapon equippedWeapon;
    [SerializeField] Transform shootingPosition;

    [SerializeField] LayerMask _ignoreLayer, _targetLayer;

    public LayerMask TargetLayer => _targetLayer;
    public LayerMask IgnoreLayer => _ignoreLayer;

    void Start()
    {
        equippedWeapon = Object.Instantiate(equippedWeapon);
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
            
            if (!equippedWeapon.FireProjectile())
            {
                Debug.Log("Reloading");
                reloadContdown = equippedWeapon.ReloadTime;
            }
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
