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

    private WaveSpawner spawner;
    [SerializeField] private PowerSpawner ps;
    [SerializeField] AIAbility[] abilities;
    private float abilityCD = 0;

    public EnemyHealthBar enemyHealthBar;
    private void Start()
    {
        this.ai = GetComponent<AI>();
        stats = new EnemyStats(this, stats.Health, stats.Shield, stats.Speed);
    }

    private void Update()
    {
        abilityCD -= Time.deltaTime;
    }
    public void UseWeapon()
    {
        weaponController.Fire();
    }

    public bool UseAbility(int index, Transform target)
    {
        if (abilityCD <= 0 && abilities[index] != null)
        {
            StartCoroutine(abilities[index].Activate(this, target));
            abilityCD = abilities[index].CoolDown;
            return true;
        }
        return false;
    }

    public override void TakeDamage(int amount)
    {
        stats.TakeDamage(amount, enemyHealthBar);
    }

    public override void GainHealth(int amount)
    {
        stats.GainHealth(amount);
    }

    public void IncreaseLevel(int level)
    {
        stats.IncreaseMaxHealth(10 * level);
        weaponController.equippedWeapon.IncreasePower(Pickup_weaponPower.BuffType.Damage, 2f);
    }

    public override void Die()
    {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        spawner.enemiesAlive.Remove(this.gameObject);
        ps.PowerGenerator(transform);
        Destroy(gameObject);
    }

    public override void AffectSpeed(int amount)
    {
        stats.SetSpeed(amount);
    }
    public void SetSpawner(WaveSpawner spawner)
    {
        this.spawner = spawner;
    }

    public override IEnumerator Regen(int amountToRegen, float regenSpeed)
    {
        while (amountToRegen > 0 || Channeling == true)
        {
            stats.GainHealth(5);
            amountToRegen -= 5;
            yield return new WaitForSeconds(regenSpeed);
        }

    }
}
