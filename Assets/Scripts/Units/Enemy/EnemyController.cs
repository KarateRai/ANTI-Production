using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    ///-------------------------Variables-------------------------------///
    [Header("Unit Stats")]
    [SerializeField] EnemyStats stats;
    public EnemyStats Stats => stats;

    [HideInInspector] public AI ai;

    [Header("Animation")]
    public Animator animator;

    [HideInInspector] public GameObject fromObjPosition;
    [HideInInspector] public GameObject toObjPosition;
    Rigidbody rb;
    private void Start()
    {
        this.ai = GetComponent<AI>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(stats.Speed, 0);
    }
    public void UseWeapon()
    {
        weaponController.Fire();
    }

    public void UseAbility()
    {
        Debug.Log("Using ability");
    }

    public override void TakeDamage(int amount)
    {
        stats.TakeDamage(amount);
    }

    public override void GainHealth(int amount)
    {
        stats.GainHealth(amount);
    }

    public override void Die()
    {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Debug.Log("Trying to destroy: " + gameObject);
        Destroy(gameObject);
    }

    public override void AffectSpeed(int amount)
    {
        stats.SetSpeed(amount);
    }
}
