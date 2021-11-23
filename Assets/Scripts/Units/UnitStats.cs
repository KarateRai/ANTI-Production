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
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxSpeed;
    public int Health => _health;
    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;

    protected int level = 1;

    public UnitStats(UnitController controller, int health, float speed)
    {
        this.controller = controller;
        this._health = health;
        this._speed = speed;
        u_health = health;
        u_speed = speed;
        u_level = level;
    }
   
    //Could return bool for credit?
    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;

        //healthBar.fillAmount = health / u_health;
        if (_health <= 0)
        {
            _health = 0;
            controller.Die();
        }
    }
    public bool GainHealth(int amount)
    {
        if (_health == u_health)
            return false;

        _health += amount;
        if (_health > u_health)
        {
            _health = u_health;
        }
        return true;
    }

    public void SetSpeed(int amount)
    {
        if (_speed + amount >= _maxSpeed || _speed + amount <= 0)
        {
            _speed = amount < 0 ? _speed = 0 : _speed = _maxSpeed;
        }
        else
        {
            _speed += amount;
        }
    }

    public void Slow(float amount)
    {
        _speed = u_speed * (1f - amount);
    }

    public void ResetSpeed()
    {
        _speed = u_speed;
    }
}
