using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInterpreter : MonoBehaviour
{
    // info
    // firepoint -> get comp. <tag? name? "FirePoint"> alt. hårdkodat i editorn.
    // player position = this.transform.postition
    // 


     
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
        if (ability.prefab != null)
        {
            var attack = Instantiate(ability.prefab, GetComponent<Transform>().position, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up));
            
        }
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
