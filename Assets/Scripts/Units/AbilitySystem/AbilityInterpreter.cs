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
            //loop through array of abilityattacks and look at parameters inside each abilityattack
            for (int i = 0; i < ability.abilityAttacks.Length; i++)
            {
                if (ability.abilityAttacks[i].attackType == AbilityAttack.AttackType.RANGED)
                {
                    GameObject attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                    testAttack_script ins_script = attack.GetComponent<testAttack_script>();

                    ins_script.damage = ability.abilityAttacks[i].damageModifier;
                    ins_script.range = ability.abilityAttacks[i].splashRadius;
                }
                else if (ability.abilityAttacks[i].attackType == AbilityAttack.AttackType.AOE)
                {

                    GameObject attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                    ParticleLauncher ins_script = attack.GetComponent<ParticleLauncher>();
                    ins_script.Amount = ability.abilityAttacks[i].damageModifier;
                    //ins_script.Range = ability.abilityAttacks[i].splashRadius;
                    for (int j = 0; j < ability.abilityEffects.Length; j++)
                    {

                    }
                }
            }
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
            for (int i = 0; i < ability.abilityAttacks.Length; i++)
            {
                var heal = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                ParticleLauncher ins_script = heal.GetComponent<ParticleLauncher>();
                ins_script.Amount = ability.abilityAttacks[i].damageModifier;
            }
        }
    }
}
