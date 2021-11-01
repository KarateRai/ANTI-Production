using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    ///-------------------------Variables-------------------------------///
    [Header("Unit Stats")]
    [SerializeField] EnemyStats stats;
    public EnemyStats Stats => stats;

    [Header("AI")]
    [HideInInspector]
    public AI ai;


    [HideInInspector] public GameObject fromObjPosition;
    /*[HideInInspector]*/ public GameObject toObjPosition;

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
