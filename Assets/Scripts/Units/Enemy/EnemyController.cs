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
    private float abilityCD = 0;

    public EnemyHealthBar enemyHealthBar;
    private void Start()
    {
        this.ai = GetComponent<AI>();
        stats = new EnemyStats(this, stats.Health, stats.Shield, stats.Speed, stats.MaxSpeed, enemyHealthBar);
        weaponController.equippedWeapon = Object.Instantiate(weaponController.equippedWeapon);
        enemyHealthBar.UpdateArmor(stats.Shield);
        enemyHealthBar.SetImmediateArmor(stats.Shield);
        enemyHealthBar.SetImmediateHealth(stats.Health);
        IncreaseLevel(spawner.waveNumber);

    }

    private void Update()
    {
        abilityCD -= Time.deltaTime;
        if(Channeling)
        {
            animator.SetBool("Channeling", true);
        }
        else
        {
            animator.SetBool("Channeling", false);
        }
    }
    public void UseWeapon()
    {
        weaponController.Fire();
    }
    public void UseWeapon(GameObject target)
    {
        //Solotarget
        weaponController.Fire(target);
    }

    public bool UseAbility(int index, Transform target)
    {
        if (abilityCD <= 0 && ai.abilities[index] != null)
        {
            StartCoroutine(ai.abilities[index].Activate(this, target));
            abilityCD = ai.abilities[index].CoolDown;
            return true;
        }
        return false;
    }

    public override void TakeDamage(int amount)
    {
        GUIManager.instance.NewFloatingCombatText(amount, true, transform.position, true);
        stats.TakeDamage(amount);
    }

    public override void GainHealth(int amount)
    {
        GUIManager.instance.NewFloatingCombatText(amount, false, transform.position, true);
        stats.GainHealth(amount);
    }

    private void IncreaseLevel(int level)
    {
        
        if (level == 0 || level == 1)
        {
            return;
        }
        stats.IncreaseMaxHealth(10 * level);
        weaponController.equippedWeapon.IncreasePower(Pickup_weaponPower.BuffType.Damage, 2f);
    }

    public override void Die()
    {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        spawner.RemoveEnemy(this.gameObject);
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
    public override void Regen(int amountToRegen, float regenSpeed)
    {
        StartCoroutine(RegenCoroutine(amountToRegen, regenSpeed));
    }
    public IEnumerator RegenCoroutine(int amountToRegen, float regenSpeed)
    {
        while (amountToRegen > 0 && Stats.Shield > 0)
        {
            stats.GainHealth(5);
            amountToRegen -= 5;
            yield return new WaitForSeconds(regenSpeed);
        }

    }
}
