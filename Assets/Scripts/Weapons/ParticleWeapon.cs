using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Particle Weapon")]
public class ParticleWeapon : Weapon
{
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
    public override void Init(Transform shootingPos, LayerMask targetLayer, Gradient gradient)
    {
        this.shootingPos = shootingPos;
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        if (!isExplosive)
            _particleProjectile.transform.parent = shootingPos;
        _particleProjectile.transform.position = shootingPos.position;
        _particleProjectile.transform.rotation = shootingPos.rotation;
        var collision = _particleProjectile.collision;
        collision.collidesWith = targetLayer;
        psMain = _particleProjectile.main;
        psMain.startColor = gradient;
    }

    public override bool Fire()
    {
        if (isExplosive)
        {
            _particleProjectile.transform.position = shootingPos.position;
            _particleProjectile.Play();
            return true;
        }

        if (_bullets == 0)
        {
            //Fire
            emitParams.velocity = shootingPos.forward * _bulletSpeed;
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
                _crit -= amount;
                break;
            case Pickup_weaponPower.BuffType.BulletsFired:
                Debug.Log(_bulletsToShoot);
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
                _crit += amount;
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

        //switch (color)
        //{
        //    case PlayerChoices.OutfitChoice.BLUE:
        //        psMain.startColor = new ParticleSystem.MinMaxGradient(Color.blue);
        //        break;
        //    case PlayerChoices.OutfitChoice.GREEN:
        //        psMain.startColor = new ParticleSystem.MinMaxGradient(Color.green);
        //        break;
        //    case PlayerChoices.OutfitChoice.YELLOW:
        //        psMain.startColor = new ParticleSystem.MinMaxGradient(Color.yellow);
        //        break;
        //    case PlayerChoices.OutfitChoice.ORANGE:
        //        psMain.startColor = new ParticleSystem.MinMaxGradient(
        //            new Color(0.2F, 0.3F, 0.4F));
        //        break;
        //    case PlayerChoices.OutfitChoice.RED:
        //        psMain.startColor = new ParticleSystem.MinMaxGradient(Color.red);
        //        break;
        //    case PlayerChoices.OutfitChoice.PURPLE:
        //        psMain.startColor = new ParticleSystem.MinMaxGradient(
        //            new Color(0.3F, 0.1F, 0.6F));
        //        break;

        //}
    }
}
