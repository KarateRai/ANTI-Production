using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : UnitStats
{
   
    public EnemyStats(UnitController controller, int health, int shield, float speed) : base(controller, health, shield, speed)
    {
        u_health = this._health;
        u_speed = this._speed;
    }

    public void TakeDamage(int damageAmount, EnemyHealthBar healthBar)
    {
        _shield -= damageAmount;
        if (_shield > 0)
            return;
        else
        {
            damageAmount = -_shield;
            _shield = 0;

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
}
