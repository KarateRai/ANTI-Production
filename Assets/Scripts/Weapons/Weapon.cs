using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Regular Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] float _damage;
    [SerializeField] float _firerate;
    [SerializeField] float _reloadTime;
    [SerializeField] int _bulletsToShoot;
    [SerializeField] int _bullets;
    private int _bulletsFired;
    //[SerializeField] GameObject _projectilePrefab;
    [SerializeField] ParticleSystem _particleProjectilePrefab;
    private ParticleSystem _particleProjectile;
    public float Damage => _damage;
    public float Firerate => _firerate;
    public float ReloadTime => _reloadTime;
    public int BulletsFired => _bulletsFired;
    public int Bullets => _bullets;
    //public GameObject Projectile => _projectilePrefab;
    public ParticleSystem ParticleProjectile => _particleProjectile;
    public void Init(Transform t, LayerMask targetLayer)
    {
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        _particleProjectile.transform.parent = t;
        _particleProjectile.transform.position = t.position;
        _particleProjectile.transform.rotation = t.rotation;
        var collision = _particleProjectile.collision;
        collision.collidesWith = targetLayer;
    }

    public bool FireProjectile()
    {
        if (_bulletsFired == _bullets)
        {
            //reload
            _bulletsFired = 0;
            return false;
        }
        else
        {
            if (_bulletsFired + _bulletsToShoot >= _bullets)
            {
                _particleProjectile.Emit(_bullets - _bulletsFired);
                _bulletsFired = _bullets;
            }
            else
            {
                _particleProjectile.Emit(_bulletsToShoot);
                _bulletsFired += _bulletsToShoot;
            }
            return true;
        }
        
    }

    public Weapon GetWeapon()
    {
        return this;
    }
}
