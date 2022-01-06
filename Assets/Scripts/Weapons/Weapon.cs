using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : ScriptableObject
{
    WeaponStats stats;
    //Weapon variables
    [SerializeField] protected int _damage;
    [SerializeField] protected int _maxDamage;
    [SerializeField] protected float _firerate;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _bulletsToShoot;
    [SerializeField] protected int _bullets;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _crit;
    [SerializeField] protected WeaponAbility _ability;
    protected int _bulletsFired;

    //Weapon variable GETTERS
    public int Damage => (int)(Random.value <= Crit / 100f ? Random.Range(_damage, _maxDamage) * (1.5f + OverflowingCrit) : Random.Range(_damage, _maxDamage));
    public float Firerate => _firerate;
    public float ReloadTime => _reloadTime;
    public float BulletsToShoot => _bulletsToShoot;
    public int BulletsFired => _bulletsFired;
    public int Bullets => _bullets;
    public float BulletSpeed => _bulletSpeed;
    public float Crit => _crit;
    private float OverflowingCrit => Crit > 100 ? ((Crit - 100) / 100) : 0;
    public float ChargeTime => _ability.ChargeTime;
    public float PowerTime => _ability.PowerTime;
    public WeaponAbility Ability => _ability;
    public bool AbilityAvaiable => _ability != null;


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

    public void ResetStats()
    {
        _damage = stats.damage;
        _maxDamage = stats.maxDamage;
        _firerate = stats.firerate;
        _reloadTime = stats.reloadtime;
        _bulletsToShoot = stats.bulletstoshoot;
        _bullets = stats.bullets;
        _bulletSpeed = stats.bulletspeed;
        _crit = stats.crit;
        _ability = stats.ability;
    }
    public WeaponStats GetStats()
    {
        return stats;
    }
    public void SetStats()
    {
        stats = new WeaponStats(_damage, _maxDamage, _firerate, _reloadTime, _bulletsToShoot, _bullets, _bulletSpeed, _crit, _ability);
    }

    public void SetAbility()
    {
        _ability = Object.Instantiate(_ability);
    }
}
public struct WeaponStats
{
    public int damage;
    public int maxDamage;
    public float firerate;
    public float reloadtime;
    public int bulletstoshoot;
    public int bullets;
    public float bulletspeed;
    public float crit;
    public WeaponAbility ability;
    public WeaponStats(int damage, int maxdamage, float firerate, float reloadtime, int bulletstoshoot, int bullets, float bulletspeed, float crit, WeaponAbility ability)
    {
        this.damage = damage;
        this.maxDamage = maxdamage;
        this.firerate = firerate;
        this.reloadtime = reloadtime;
        this.bulletstoshoot = bulletstoshoot;
        this.bullets = bullets;
        this.bulletspeed = bulletspeed;
        this.crit = crit;
        this.ability = ability;
    }
}
