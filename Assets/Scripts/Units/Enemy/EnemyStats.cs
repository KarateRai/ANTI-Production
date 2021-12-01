using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : UnitStats
{
   
    public EnemyStats(UnitController controller, int health, int shield, float speed, float maxSpeed) : base(controller, health, shield, speed, maxSpeed)
    {
        u_health = this._health;
        u_speed = this._speed;
    }

    public void TakeDamage(int damageAmount, EnemyHealthBar healthBar)
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
}
