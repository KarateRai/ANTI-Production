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
}
