using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityEffect
{
    public enum EffectTarget
    {
        SELF,
        SELF_AOE,
        ENEMY,
        ENEMY_AOE
    }
    public EffectTarget effectTarget;
    public enum EffectType
    {
        NONE,
        HEAL,
        REMOVE_BUFF,
        REMOVE_DEBUFF,
        ATTACK_UP,
        ATTACK_DOWN,
        DEFENSE_UP,
        DEFENSE_DOWN,
        SPEED_UP,
        SPEED_DOWN,
        DASH,
        STUN,
        POISON,
        SILENCE,
        TAUNT
    }
    public EffectType effectType;
    public float effectModifier;
    public float effectRange;
    public int effectDuration;
    [Range(0f, 1f)]
    public float effectChance = 1f;
}
