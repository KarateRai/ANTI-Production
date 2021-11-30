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
        /*
        //Attack types involves abl_TestAttack & abl_TestEMP

            abl_TestAttack:
                - Basic Missle attack
                - Ability attacks: Ranged, Directional
            abl_TestEMP:
                - AoE damage & slow attack
                - Ability attacks: Ranged, Directional
        */
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {
            //loop through array of abilityattacks and look at parameters inside each abilityattack
            for (int i = 0; i < ability.abilityAttacks.Length; i++)
            {
                if (ability.abilityAttacks[i].attackType == AbilityAttack.AttackType.RANGED)
                {
                    //Instantiate the Ability---------------------------------------------------------------------------//
                    GameObject attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                    //Define attached script, used to pass on the damage amount and radius------------------------------//
                    testAttack_script ins_script = attack.GetComponent<testAttack_script>();
                    ins_script.damage = ability.abilityAttacks[i].damageModifier;
                    ins_script.range = ability.abilityAttacks[i].splashRadius;
                    //--------------------------------------------------------------------------------------------------//
                } // If ranged: DO THIS
                else if (ability.abilityAttacks[i].attackType == AbilityAttack.AttackType.AOE)
                {
                    //Instantiate the Ability and set the parent to the player, this way the ability follows the player.
                    GameObject attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                    attack.transform.parent = gameObject.transform;
                    //--------------------------------------------------------------------------------------------------//
                    //Define attached script, used to pass on the damage amount-----------------------------------------//
                    emp_test ins_script = attack.GetComponent<emp_test>();
                    ins_script.damage = (int)ability.abilityAttacks[i].damageModifier;
                    ins_script.range = (int)ability.abilityAttacks[i].splashRadius;
                    //ins_script.Range = ability.abilityAttacks[i].splashRadius;
                    //--------------------------------------------------------------------------------------------------//
                    for (int j = 0; j < ability.abilityEffects.Length; j++)
                    {

                    }
                } // IF AOE: DO THIS.
            }
        }
            
    }
    private void ProcessBuffType(Ability ability)
    {
        for (int i = 0; i < ability.abilityEffects.Length; i++)
        {
            if (ability.abilityEffects[i].effectTarget == AbilityEffect.EffectTarget.SELF)
            {
                //DASH-------------------------------------------------------------------------------//
                //Sets speed to maxspeed, needs to be reset
                PlayerController playerController = gameObject.GetComponent<PlayerController>();
                PlayerStats playerStats = playerController.stats;
                playerController.AffectSpeed((int)playerStats.MaxSpeed);
                StartCoroutine(playerController.DelayedResetSpeed(ability.abilityEffects[i].effectDuration));
                //-----------------------------------------------------------------------------------//
            }
            else
            {
                //if target is other entity, find entity and apply logic.
            }
        }
    }
    private void ProcessDeBuffType(Ability ability)
    {
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        for (int i = 0; i < ability.abilityEffects.Length; i++)
        {
            if (ability.abilityEffects[i].effectTarget == AbilityEffect.EffectTarget.SELF_AOE)
            {
                GameObject attack = Instantiate(ability.prefab, firePoint, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                attack.transform.parent = gameObject.transform;
                //--------------------------------------------------------------------------------------------------//
                //Define attached script, used to pass on the damage amount-----------------------------------------//
                test_taunt ins_script = attack.GetComponent<test_taunt>();
                ins_script.tauntTarget = gameObject;
                ins_script.duration = (int)ability.abilityEffects[i].effectDuration;
                ins_script.range = (int)ability.abilityEffects[i].effectRange;
            }
            else
            {
                //if target is other entity, find entity and apply logic.
            }
        }
    }
    private void ProcessHealType(Ability ability)
    {
        Vector3 firePoint = new Vector3(GetComponent<Transform>().position.x + GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y + 1, GetComponent<Transform>().position.z + +GetComponent<Transform>().forward.z);
        if (ability.prefab != null)
        {
            for (int i = 0; i < ability.abilityEffects.Length; i++)
            {
                GameObject heal = Instantiate(ability.prefab, this.transform.position, Quaternion.LookRotation(GetComponent<Transform>().forward, Vector3.up) * ability.prefab.transform.rotation);
                heal.transform.parent = gameObject.transform;
                test_heal ins_script = heal.GetComponent<test_heal>();
                ins_script.amount = (int)ability.abilityEffects[i].effectModifier;
                ins_script.range = (int)ability.abilityEffects[i].effectRange;
            }
        }
    }
}
