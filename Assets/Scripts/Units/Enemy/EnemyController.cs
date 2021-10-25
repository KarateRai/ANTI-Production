using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    ///-------------------------Variables-------------------------------///

    AI ai;
    EnemyStats stats;
    public GameObject fromObjPosition;
    public GameObject toObjPosition;

    //AI needs a goal to move to, set in awake/start before initializing AI
    public Transform testT;

    public void Awake()
    {
        ai = new BT_AI();
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
