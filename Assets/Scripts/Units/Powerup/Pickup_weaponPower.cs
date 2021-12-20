using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Weapon Pickup")]
public class Pickup_weaponPower : Pickup_item
{
    public enum BuffType
    {
        Size,
        Firerate,
        Crit,
        BulletsFired,
        BulletSpeed,
        Damage
    }

    [SerializeField] float amountInPercent;
    [SerializeField] BuffType type;

    public override bool Use(Collider player)
    {
        Weapon weapon = player.GetComponentInParent<WeaponController>().equippedWeapon;
       
        weapon.IncreasePower(type, amountInPercent);
        return true;
    }
    public override void Remove(Collider player)
    {
        Weapon weapon = player.GetComponentInParent<WeaponController>().equippedWeapon;
        weapon.DecreasePower(type, amountInPercent);
    }
}
