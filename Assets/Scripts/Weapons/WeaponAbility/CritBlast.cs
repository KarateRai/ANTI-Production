using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Ability/CritBlast")]
public class CritBlast : WeaponAbility
{
    private void OnEnable()
    {
        Activated = false;
    }
    public override void Activate(Weapon weapon)
    {
        if (ChargesLeft == 0 && Activated == true)
        {
            Deactivate(weapon);
            return;
        }

        if (Activated == true)
            return;

        Activated = true;
        ChargesLeft = _charges;
        weapon.IncreasePower(Pickup_weaponPower.BuffType.Crit, amount);
    }

    public override void Deactivate(Weapon weapon)
    {
        if (Activated == false)
            return;

        Activated = false;
        weapon.DecreasePower(Pickup_weaponPower.BuffType.Crit, amount);
    }
}
