using UnityEngine;
using UnityEngine.UI;

public abstract class UnitStats
{
    protected UnitController controller;
       
    ///--------------------Unit Start Attributes-----------------------///

    protected int u_health;

    protected float u_speed;

    protected int u_level;

    ///--------------------Unit Attributes----------------------------///
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected int level = 1;
    private Transform takeDamagePosition;

    public UnitStats(UnitController controller)
    {
        this.controller = controller;
        u_health = health;
        u_speed = speed;
        u_level = level;
        if (controller.takeDamagePosition == null)
        {
            takeDamagePosition = controller.transform;
        }
        else
        {
            this.takeDamagePosition = controller.takeDamagePosition;
        }
    }
   
    //Could return bool for credit?
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        //healthBar.fillAmount = health / u_health;
        if (health <= 0)
        {
            health = 0;
            controller.Die();
        }
    }
    public bool GainHealth(int amount)
    {
        if (health == u_health)
            return false;
        health += amount;
        if (health > u_health)
        {
            health = u_health;
        }
        return true;
    }

    public void Slow(float amount)
    {
        speed = u_speed * (1f - amount);
    }

}
