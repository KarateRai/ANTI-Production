using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] float _firerate;
    [SerializeField] float _reloadTime;
    [SerializeField] float _spread;
    [SerializeField] int _bulletsFired;
    [SerializeField] int _bullets;
    [SerializeField] GameObject _projectilePrefab;
    public float Damage => _damage;
    public float Firerate => _firerate;
    public float ReloadTime => _reloadTime;
    public float Spread => _spread;
    public int BulletsFired => _bulletsFired;
    public int Bullets => _bullets;
    public GameObject Projectile => _projectilePrefab;

    public Weapon GetWeapon()
    {
        return this;
    }
}
