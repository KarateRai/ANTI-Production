using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : UnitStats
{
    EnemyHealthBar healthBar;
   
    public EnemyStats(UnitController controller, int health, int shield, float speed, float maxSpeed, EnemyHealthBar healthBar) : base(controller, health, shield, speed, maxSpeed)
    {
        u_health = this._health;
        u_speed = this._speed;
        this.healthBar = healthBar;
    }

    public override void TakeDamage(int damageAmount)
    {
        //Set shield 
        _shield -= damageAmount;
        if (_shield > 0)
        {
            healthBar.UpdateArmor(GetSP());
            return;
        }
        else
        {
            damageAmount = -_shield;
            _shield = 0;
            healthBar.UpdateArmor(GetSP());
        }

        _health -= damageAmount;
        if (_health <= 0)
        {
            _health = 0;
            healthBar.UpdateHealth(GetHPP());
            controller.Die();
        }
        else
            healthBar.UpdateHealth(GetHPP());
    }
    public override bool GainHealth(int amount)
    {
        if (_health == u_health)
            return false;

        _health += amount;
        if (_health > u_health)
        {
            _health = u_health;
        }
        healthBar.UpdateHealth(_health);
        return true;
    }
}
