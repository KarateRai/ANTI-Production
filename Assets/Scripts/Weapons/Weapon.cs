using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Regular Weapon")]
public class Weapon : ScriptableObject
{
    private Transform t;
    private ParticleSystem.MainModule psMain;

    [SerializeField] float _damage;
    [SerializeField] float _firerate;
    [SerializeField] float _reloadTime;
    [SerializeField] int _bulletsToShoot;
    [SerializeField] int _bullets;
    [SerializeField] float _bulletSpeed;
    private int _bulletsFired;
    //[SerializeField] GameObject _projectilePrefab;
    [SerializeField] ParticleSystem _particleProjectilePrefab;
    private ParticleSystem _particleProjectile;
    public float Damage => _damage;
    public float Firerate => _firerate;
    public float ReloadTime => _reloadTime;
    public int BulletsFired => _bulletsFired;
    public int Bullets => _bullets;
    public float BulletSpeed => _bulletSpeed;
    //public GameObject Projectile => _projectilePrefab;
    public ParticleSystem ParticleProjectile => _particleProjectile;
    public void Init(Transform t, LayerMask targetLayer)
    {
        this.t = t;
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        _particleProjectile.transform.parent = t;
        _particleProjectile.transform.position = t.position;
        _particleProjectile.transform.rotation = t.rotation;
        var collision = _particleProjectile.collision;
        collision.collidesWith = targetLayer;
        psMain = _particleProjectile.main;
    }
    public void Init(Transform t, LayerMask targetLayer, Gradient gradient)
    {
        this.t = t;
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        _particleProjectile.transform.parent = t;
        _particleProjectile.transform.position = t.position;
        _particleProjectile.transform.rotation = t.rotation;
        var collision = _particleProjectile.collision;
        collision.collidesWith = targetLayer;
        psMain = _particleProjectile.main;
        psMain.startColor = gradient;
    }

    public bool FireProjectile()
    {
        if (_bulletsFired == _bullets)
        {
            //Reload
            _bulletsFired = 0;
            return false;
        }
        else
        {
            //Fire
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = t.position;
            emitParams.velocity = t.forward * _bulletSpeed;
            Debug.Log("Forward: " + t.forward);
            Debug.Log("Params: " + emitParams.velocity);
            if (_bulletsFired + _bulletsToShoot >= _bullets)
            {
                _particleProjectile.Emit(emitParams, _bullets - _bulletsFired);
                _bulletsFired = _bullets;
            }
            else
            {
                _particleProjectile.Emit(emitParams, _bulletsToShoot);
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
