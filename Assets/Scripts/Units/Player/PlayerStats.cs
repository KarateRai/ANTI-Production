using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : UnitStats
{
    
    private int id_number;

    public PlayerStats(PlayerController controller, int health, float speed) : base(controller, health, speed) 
    {
        u_health = this._health;
        u_speed = this.Speed;
        Debug.Log("Uhealth / USpeed = " + u_health + "/" + u_speed + " in " + this.ToString());
    }

    public int idNumber { get { return id_number; } set { id_number = value; } }
    public int Level { get { return level; } set { level = value; } }

    public void Initialize()
    {
        u_health = _health;
        u_speed = _speed;
        u_level = level;
    }
    public int GetHPP()
    {
        //int hpp = (int)(((float)health / (float)u_health) * 100f);
        //return hpp;
        return Health;
    }
    public void ResetHealth()
    {
        _health = u_health;
    }
}
