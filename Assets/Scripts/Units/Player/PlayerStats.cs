using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    private int id_number;
    public int idNumber { get { return id_number; } set { id_number = value; } }

    public int Health { get { return health; } set { health = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
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
    public void Spawn()
    {
        this.gameObject.SetActive(true);
        isDead = false;
    }
    public void ResetHealth()
    {
        health = u_health;
    }
    public override void Die()
    {
        isDead = true;
        //Earn gold/resources etc for killing enemy here

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        //transform.position = GameManager.Instance.stagemanager.SpawnPoint1.position;
        this.gameObject.SetActive(false);
    }
}
