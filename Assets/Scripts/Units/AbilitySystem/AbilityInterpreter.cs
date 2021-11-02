using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInterpreter : MonoBehaviour
{
    public void Interpret(Ability ability)
    {
        foreach (Ability.AbilityType type in ability.abilityTypes)
        {
            switch (type)
            {
                case Ability.AbilityType.ATTACK:
                    ProcessAttackType(ability);
                    break;
                case Ability.AbilityType.BUFF:
                    ProcessBuffType(ability);
                    break;
                case Ability.AbilityType.DEBUFF:
                    break;
                case Ability.AbilityType.HEAL:
                    break;
            }
        }
    }


    private void ProcessAttackType(Ability ability)
    {
        
    }
    private void ProcessBuffType(Ability ability)
    {
        
    }
    private void ProcessDeBuffType(Ability ability)
    {

    }
    private void ProcessHealType(Ability ability)
    {

    }
}
