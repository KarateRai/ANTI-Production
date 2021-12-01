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

    public void TakeDamage(int damageAmount)
    {
        //Set shield 
        _shield -= damageAmount;
        if (_shield > 0)
        {
            healthBar.UpdateArmor(_shield);
            return;
        }
        else
        {
            damageAmount = -_shield;
            _shield = 0;
            healthBar.UpdateArmor(_shield);
        }

        _health -= damageAmount;
        if (_health <= 0)
        {
            _health = 0;
            healthBar.UpdateHealth(_health);
            controller.Die();
        }
        else
            healthBar.UpdateHealth(_health);
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
