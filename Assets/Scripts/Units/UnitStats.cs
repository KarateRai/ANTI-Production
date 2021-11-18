using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class UnitStats
{
    protected UnitController controller;
       
    ///--------------------Unit Start Attributes-----------------------///

    protected int u_health;

    protected float u_speed;

    protected int u_level;

    ///--------------------Unit Attributes----------------------------///
    [SerializeField] protected int health;
    public int Health => health;
    [SerializeField] protected float speed;
    public float Speed => speed;
    protected int level = 1;


    public UnitStats(UnitController controller, int health, float speed)
    {
        this.controller = controller;
        this.health = health;
        this.speed = speed;
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

    public void GainSpeed(int amount)
    {
        speed += amount;
    }

    public void Slow(float amount)
    {
        speed = u_speed * (1f - amount);
    }

    public void ResetSpeed()
    {
        speed = u_speed;
    }
}
