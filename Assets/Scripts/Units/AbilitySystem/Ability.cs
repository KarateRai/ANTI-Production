using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Players/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;
    public float cooldownTime;
    public Sprite abilityIcon;
    public GameObject prefab;
    public enum AbilityType
    {
        ATTACK,
        BUFF,
        DEBUFF,
        HEAL
    }
    public AbilityType[] abilityTypes;
    public AbilityAttack[] abilityAttacks;
    public AbilityEffect[] abilityEffects;
}
