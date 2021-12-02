using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : UnitController
{
    public Tower baseTower;
    public int health;

    public override void GainHealth(int amount)
    {
        health += amount;
    }
    public override void Regen(int amountToRegen, float regenSpeed)
    {
        return;
    }
    public override void TakeDamage(int amount)
    {
        health -= amount;
    }
    public override void AffectSpeed(int amount)
    {
        return;

    }
    // Start is called before the first frame update
    void Start()
    {
        baseTower = gameObject.GetComponent<Tower>();
    }

    public override void Die()
    {
        baseTower.Delete();
        base.Die();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
}
