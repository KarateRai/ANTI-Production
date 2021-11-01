using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] LayerMask _targetLayer;
    [SerializeField] LayerMask _ignoreLayer;
    public LayerMask TargetLayer => _targetLayer;
    public LayerMask IgnoreLayer => _ignoreLayer;

    private float attackCountdown = 0;

    public Weapon equippedWeapon;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Fire();
        }
        attackCountdown -= Time.deltaTime;
    }

    public void Fire()
    {
        if (attackCountdown <= 0)
        {
            GameObject bullet = BulletObjectPool.SharedInstance.GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
            else
            {
                Debug.LogError("Out of ammo");
            }

            attackCountdown = 1f / equippedWeapon.Firerate;
        }
    }

}
