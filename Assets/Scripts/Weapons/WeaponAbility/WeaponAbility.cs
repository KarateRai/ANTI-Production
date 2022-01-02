using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAbility : ScriptableObject
{
    protected bool Activated { get; set; }

    public abstract void Activate(Weapon weapon);
    public abstract void Deactivate(Weapon weapon);
}
