using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : UnitStats
{
    
    private int id_number;

    public PlayerStats(PlayerController controller, int health, int shield, float speed, float maxSpeed) : base(controller, health, shield, speed, maxSpeed) 
    {
        u_health = this._health;
        u_speed = this.Speed;
    }

    public int idNumber { get { return id_number; } set { id_number = value; } }
    public int Level { get { return level; } set { level = value; } }

    public void Initialize()
    {
        u_health = _health;
        u_speed = _speed;
        u_level = level;
    }
   
    public void ResetHealth()
    {
        _health = u_health;
    }
}
