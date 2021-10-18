using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    ///-------------------------Variables-------------------------------///

    AI ai;
    EnemyStats stats;

    public void Awake()
    {
        ai.InitializeAI(this);
    }

    void Update()
    {
        ai.Tick();
    }

    public void UseWeapon()
    {
        Debug.Log("Using Weapon");
    }

    public void UseAbility()
    {
        Debug.Log("Using ability");
    }

    public override void TakeDamage(int amount)
    {
        Debug.Log("Taking damage");
    }

    public override void GainHealth(int amount)
    {
        Debug.Log("Gaining health");
    }

}
