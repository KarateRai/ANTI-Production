using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityInterpreter))]
public class UnitAbilities : MonoBehaviour
{
    private AbilityInterpreter interpreter;
    [HideInInspector]
    public UnitRole unitRole;
    private List<float> activeCooldowns;
    private List<bool> abilitiesAvailable;
    private void Awake()
    {
        interpreter = GetComponent<AbilityInterpreter>();
    }
    private void Start()
    {
        AddCooldowns();
    }

    private void Update()
    {
        CooldownTic();
    }
    public void AddCooldowns()
    {
        activeCooldowns = new List<float>();
        foreach (Ability ability in unitRole.abilities)
        {
            activeCooldowns.Add(0f);
        }
    }

    public void ActivateAbility(int roleAbilityIndex)
    {
        if (activeCooldowns[roleAbilityIndex] <= 0)
        {
            interpreter.Interpret(unitRole.abilities[roleAbilityIndex]);
            activeCooldowns[roleAbilityIndex] = unitRole.abilities[roleAbilityIndex].cooldownTime;
        }
    }
    private void CooldownTic()
    {
        for (int i = 0; i < activeCooldowns.Count; i++)
        {
            if (activeCooldowns[i] > 0)
            {
                activeCooldowns[i] -= Time.deltaTime;
            }
            else
            {
                activeCooldowns[i] = 0;
            }
        }
    }
    public void StartCooldown(int abilityIndex)
    {
        activeCooldowns[abilityIndex] = unitRole.abilities[abilityIndex].cooldownTime;
    }

    public bool IsAbilityReady(int abilityIndex)
    {
        return activeCooldowns[abilityIndex] <= 0;
    }
}
