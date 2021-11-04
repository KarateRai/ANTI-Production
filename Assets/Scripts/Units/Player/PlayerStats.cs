using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    
    private int id_number;

    public PlayerStats(PlayerController controller) : base(controller) { }

    public int idNumber { get { return id_number; } set { id_number = value; } }
    public int Level { get { return level; } set { level = value; } }

    public void Initialize()
    {
        u_health = health;
        u_speed = speed;
        u_level = level;
    }
    public int GetHPP()
    {
        int hpp = (int)(((float)health / (float)u_health) * 100f);
        return hpp;
    }
    public void ResetHealth()
    {
        health = u_health;
    }
    
}
