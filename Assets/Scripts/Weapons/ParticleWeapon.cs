using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Particle Weapon")]
public class ParticleWeapon : Weapon
{
    private const float spread = 0.05f;
    private Vector3 shootDirection;
    private Transform shootingPos;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.EmitParams emitParams;
    private ParticleLauncher pL;
    public bool isExplosive = false;

    //[SerializeField] GameObject _projectilePrefab;
    //public GameObject Projectile => _projectilePrefab;
    public override void Init(Transform shootingPos, LayerMask targetLayer)
    {
        this.shootingPos = shootingPos;
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        if (!isExplosive)
            _particleProjectile.transform.parent = shootingPos;
        _particleProjectile.transform.position = shootingPos.position;
        _particleProjectile.transform.rotation = shootingPos.rotation;
        var collision = _particleProjectile.collision;
        collision.collidesWith = targetLayer;
        pL = _particleProjectile.GetComponent<ParticleLauncher>();
        pL.Weapon = this;
        psMain = _particleProjectile.main;
        emitParams = new ParticleSystem.EmitParams();
    }
    public override void Init(Transform shootingPos, LayerMask targetLayer, Material m)
    {
        this.shootingPos = shootingPos;
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        if (!isExplosive)
            _particleProjectile.transform.parent = shootingPos;
        _particleProjectile.transform.position = shootingPos.position;
        _particleProjectile.transform.rotation = shootingPos.rotation;
        var collision = _particleProjectile.collision;
        collision.collidesWith = targetLayer;
        pL = _particleProjectile.GetComponent<ParticleLauncher>();
        pL.Weapon = this;
        psMain = _particleProjectile.main;
        emitParams = new ParticleSystem.EmitParams();
        psMain.startColor = new ParticleSystem.MinMaxGradient(m.color);
    }

    public override bool Fire()
    {
        if (_ability != null)
        {
            if (_ability.Activated && _ability.ChargesLeft > 0)
                _ability.ChargesLeft--;
        }
        

        if (isExplosive)
        {
            _particleProjectile.transform.position = shootingPos.position;
            _particleProjectile.transform.rotation = shootingPos.rotation;
            _particleProjectile.Play();
            return true;
        }

        if (_bullets == 0)
        {
            shootDirection = shootingPos.forward;
            shootDirection.x += Random.Range(-spread, spread);
            //Fire
            emitParams.velocity = shootDirection * _bulletSpeed;
            _particleProjectile.Emit(emitParams, _bulletsToShoot);
            return true;
        }
        else if (_bulletsFired == _bullets)
        {
            //Reload
            _bulletsFired = 0;
            return false;
        }
        else
        {
            //Fire
            emitParams.velocity = shootingPos.forward * _bulletSpeed;

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

    public override void IncreasePower(Pickup_weaponPower.BuffType type, float amount)
    {
        switch (type)
        {
            case Pickup_weaponPower.BuffType.Size:
                ParticleSystem.MinMaxCurve size = psMain.startSize;
                psMain.startSizeMultiplier *= amount;
                break;
            case Pickup_weaponPower.BuffType.Firerate:
                _firerate *= amount;
                break;
            case Pickup_weaponPower.BuffType.Crit:
                _crit += amount;
                break;
            case Pickup_weaponPower.BuffType.BulletsFired:
                //Debug.Log(_bulletsToShoot);
                _bulletsToShoot += (int)amount;
                break;
            case Pickup_weaponPower.BuffType.BulletSpeed:
                _bulletSpeed *= amount;
                break;
            case Pickup_weaponPower.BuffType.Damage:
                _damage += (int)amount;
                _maxDamage += (int)amount;
                break;
        }
    }
    public override void DecreasePower(Pickup_weaponPower.BuffType type, float amount)
    {
        switch (type)
        {
            case Pickup_weaponPower.BuffType.Size:
                ParticleSystem.MinMaxCurve size = psMain.startSize;
                psMain.startSizeMultiplier /= amount;
                break;
            case Pickup_weaponPower.BuffType.Firerate:
                _firerate /= amount;
                break;
            case Pickup_weaponPower.BuffType.Crit:
                _crit -= amount;
                break;
            case Pickup_weaponPower.BuffType.BulletsFired:
                _bulletsFired /= (int)amount;
                break;
            case Pickup_weaponPower.BuffType.BulletSpeed:
                _bulletSpeed /= amount;
                break;
            case Pickup_weaponPower.BuffType.Damage:
                _damage -= (int)amount;
                _maxDamage -= (int)amount;
                break;
        }
    }

    public override Weapon GetWeapon()
    {
        return this;
    }

    public override bool Fire(GameObject target)
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        Vector3 direction = ((new Vector3(0, 1, 0) + target.transform.position) - shootingPos.position).normalized;

        emitParams.velocity = direction * _bulletSpeed;
        _particleProjectile.transform.position = shootingPos.position;
        _particleProjectile.transform.rotation = shootingPos.rotation;
        _particleProjectile.Emit(emitParams, 1);
        return true;
    }

    public override void SetColor(Material m)
    {
        psMain.startColor = new ParticleSystem.MinMaxGradient(m.color);
    }
}
