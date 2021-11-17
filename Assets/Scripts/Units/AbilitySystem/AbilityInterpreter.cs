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
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {
            var attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
            
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
