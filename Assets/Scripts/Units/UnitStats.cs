using UnityEngine;
using UnityEngine.UI;

public abstract class UnitStats : MonoBehaviour
{
    protected bool isDead;

    [SerializeField] protected GameObject deathEffect;

    public Image healthBar;

    ///--------------------Unit Start Attributes-----------------------///

    protected int u_health;

    protected float u_speed;

    protected int u_level;

    ///--------------------Unit Attributes----------------------------///
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected int level = 1;
    public Transform takeDamagePosition;


    private void Start()
    {
        u_health = health;
        u_speed = speed;
        u_level = level;
        isDead = false;
        if (takeDamagePosition == null)
        {
            takeDamagePosition = this.transform;
        }
    }

    //Could return bool for credit?
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        //healthBar.fillAmount = health / u_health;
        if (health <= 0 && !isDead)
        {
            health = 0;
            Die();
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

    public virtual void Die()
    {
        isDead = true;
        //Earn gold/resources etc for killing enemy here

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        //If we want to keep track of enemies spawned and alive,
        //we alter it here
        //WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
