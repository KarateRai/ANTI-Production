using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAbility : ScriptableObject
{
    [SerializeField] protected float amount;
    public bool Activated { get; protected set; }

    [SerializeField] protected int _charges;
    [SerializeField] private float _powerTime;
    [SerializeField] private float _chargeTime;
    public int ChargesLeft { get; set; }
    public float PowerTime => _powerTime;
    public float ChargeTime => _chargeTime;

    public abstract void Activate(Weapon weapon);
    public abstract void Deactivate(Weapon weapon);
}
