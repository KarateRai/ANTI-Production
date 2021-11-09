using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityAttack
{
    public enum AttackType
    {
        MELEE,
        RANGED,
        AOE,
        RANGEDAOE
    }
    public AttackType attackType;
    public enum AttackTarget
    {
        DIRECTIONAL,
        HOMING,
        SELF
    }
    public AttackTarget attackTarget;
    public float damageModifier;
    public float attackDuration;
    public float splashRadius;
}
