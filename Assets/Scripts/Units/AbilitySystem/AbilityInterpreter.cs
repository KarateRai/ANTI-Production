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
                    ProcessDeBuffType(ability);
                    break;
                case Ability.AbilityType.HEAL:
                    ProcessHealType(ability);
                    break;
            }
        }
    }


    private void ProcessAttackType(Ability ability)
    {
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {           
            GameObject attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
            testAttack_script ins_script = attack.GetComponent<testAttack_script>();
            ins_script.damage = ability.prefab.GetComponent<AbilityAttack>().damageModifier;
            ins_script.range = ability.prefab.GetComponent<AbilityAttack>().splashRadius;
        }
    }
    private void ProcessBuffType(Ability ability)
    {
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {
            var attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);

        }
    }
    private void ProcessDeBuffType(Ability ability)
    {
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {
            var attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);

        }
    }
    private void ProcessHealType(Ability ability)
    {
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {
            var attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);

        }
    }
}
