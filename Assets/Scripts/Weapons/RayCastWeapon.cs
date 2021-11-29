using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Raycast Weapon")]
public class RayCastWeapon : Weapon
{
    [SerializeField] float angle;
    private float range;
    private List<UnitController> unitsInRange;
    private Transform unitTransform;
    private Collider ownCollider;
    private LayerMask targetLayer;

    public override void Init(float range, Transform unitTransform, Collider ownCollider, LayerMask targetLayer)
    {
        _particleProjectile = Instantiate(_particleProjectilePrefab);
        _particleProjectile.transform.position = unitTransform.position;
        _particleProjectile.transform.rotation = unitTransform.rotation;
        _particleProjectile.transform.parent = unitTransform;

        this.unitTransform = unitTransform;
        this.ownCollider = ownCollider;
        this.targetLayer = targetLayer;
        this.range = range;
        unitsInRange = new List<UnitController>();
    }
    public override bool Fire()
    {
        _particleProjectile.Play();
        unitsInRange = TargetsInRange.GetControllersInRange(angle, range, unitTransform, ownCollider, targetLayer);
        foreach (UnitController controller in unitsInRange)
        {
            controller.TakeDamage(_damage);
        }
        return true;
    }
    public override Weapon GetWeapon()
    {
        return this;
    }
}
