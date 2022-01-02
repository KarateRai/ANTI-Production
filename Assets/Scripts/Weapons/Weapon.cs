using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : ScriptableObject
{

    //Weapon variables
    [SerializeField] protected int _damage;
    [SerializeField] protected int _maxDamage;
    [SerializeField] protected float _firerate;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _bulletsToShoot;
    [SerializeField] protected int _bullets;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _crit;
    [SerializeField] protected float _chargeDelay;
    [SerializeField] protected WeaponAbility _ability;
    protected int _bulletsFired;

    //Weapon variable GETTERS
    public int Damage => (int)(Random.value <= Crit / 100f ? Random.Range(_damage, _maxDamage) * 1.5f : Random.Range(_damage, _maxDamage));
    public float Firerate => _firerate;
    public float ReloadTime => _reloadTime;
    public float BulletsToShoot => _bulletsToShoot;
    public int BulletsFired => _bulletsFired;
    public int Bullets => _bullets;
    public float BulletSpeed => _bulletSpeed;
    public float Crit => _crit;
    public float ChargeDelay => _chargeDelay;

    //Particle System
    [SerializeField] protected ParticleSystem _particleProjectilePrefab;
    protected ParticleSystem _particleProjectile;

    public ParticleSystem ParticleProjectile => _particleProjectile;

    //Methods
    public abstract bool Fire();
    public abstract bool Fire(GameObject target);
    public virtual void Init(Transform transfrom, LayerMask targetLayer) { }
    public virtual void Init(Transform transfrom, LayerMask targetLayer, Gradient gradient) { }
    public virtual void Init(float range, Transform unitTransform, Collider ownCollider, LayerMask targetLayer) { }

    public virtual void IncreasePower(Pickup_weaponPower.BuffType type, float amount) { }
    public virtual void DecreasePower(Pickup_weaponPower.BuffType type, float amount) { }

    public virtual void ActivateAbility(Weapon equippedWeapon) 
    {
        if (_ability)
            _ability.Activate(equippedWeapon);
    }
    public virtual void DeactivateAbility(Weapon equippedWeapon) 
    {
        if (_ability)
            _ability.Deactivate(equippedWeapon);
    }

    public abstract Weapon GetWeapon();

    public abstract void SetColor(Material material);
}
