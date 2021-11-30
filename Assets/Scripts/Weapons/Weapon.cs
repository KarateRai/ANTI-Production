using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : ScriptableObject
{
    //Weapon variables
    [SerializeField] protected int _damage;
    [SerializeField] protected float _firerate;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _bulletsToShoot;
    [SerializeField] protected int _bullets;
    [SerializeField] protected float _bulletSpeed;
    protected int _bulletsFired;

    //Weapon variable GETTERS
    public int Damage => _damage;
    public float Firerate => _firerate;
    public float ReloadTime => _reloadTime;
    public float BulletsToShoot => _bulletsToShoot;
    public int BulletsFired => _bulletsFired;
    public int Bullets => _bullets;
    public float BulletSpeed => _bulletSpeed;

    //Particle System
    [SerializeField] protected ParticleSystem _particleProjectilePrefab;
    protected ParticleSystem _particleProjectile;

    public ParticleSystem ParticleProjectile => _particleProjectile;

    //Methods
    public abstract bool Fire();
    public virtual void Init(Transform transfrom, LayerMask targetLayer) { }
    public virtual void Init(Transform transfrom, LayerMask targetLayer, Gradient gradient) { }
    public virtual void Init(float range, Transform unitTransform, Collider ownCollider, LayerMask targetLayer) { }

    public virtual void IncreasePower(Pickup_weaponPower.BuffType type, float amount) { }
    public virtual void DecreasePower(Pickup_weaponPower.BuffType type, float amount) { }

    public abstract Weapon GetWeapon();

}
