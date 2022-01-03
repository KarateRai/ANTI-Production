using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Ability/SuperFire")]
public class SuperFire : WeaponAbility
{
    private void OnEnable()
    {
        Activated = false;
        ChargesLeft = _charges;
    }
    public override void Activate(Weapon weapon)
    {
        if (Activated == true)
            return;

        Activated = true;

        weapon.IncreasePower(Pickup_weaponPower.BuffType.Firerate, amount);
    }

    public override void Deactivate(Weapon weapon)
    {
        if (Activated == false)
            return;

        Activated = false;
        weapon.DecreasePower(Pickup_weaponPower.BuffType.Firerate, amount);
    }
}
