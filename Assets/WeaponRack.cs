using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WeaponRack
{
    static GameObject[] prefabs;
    public static void InitializeWeaponRack()
    {
        prefabs = Resources.LoadAll("Weapons").Cast<GameObject>().ToArray();
    }
    public static GameObject GetWeapon(PlayerChoices.WeaponChoice weaponChoice)
    {
        Weapon temp;
        switch (weaponChoice)
        {
            case PlayerChoices.WeaponChoice.RIFLE:
                return prefabs[0];
            case PlayerChoices.WeaponChoice.SHOTGUN:
                return prefabs[1];
        }
        return null;
    }
}
