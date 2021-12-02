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
    [SerializeField] protected int _shield;
    public int Health => _health;
    public int MaxHealth => u_health;
    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
    public int Shield => _shield;

    protected int level = 1;

    public UnitStats(UnitController controller, int health, int shield, float speed, float maxSpeed)
    {
        this.controller = controller;
        this._health = health;
        this._shield = shield;
        this._speed = speed;
        this._maxSpeed = maxSpeed;
    }
   
    public virtual void TakeDamage(int damageAmount)
    {
        _shield -= damageAmount;
        if (_shield > 0)
            return;
        else
        {
            damageAmount = -_shield;
            _shield = 0;
        }

        _health -= damageAmount;
        if (_health <= 0)
        {
            _health = 0;
            controller.Die();
        }
    }
    public int GetHPP()
    {
        return (int)(((float)Health / (float)u_health) * 100f);
    }
    public int GetSP()
    {
        return (int)(((float)Shield / (float)u_health) * 100f);
    }
    public virtual bool GainHealth(int amount)
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

    public void SetShield(int amount)
    {
        _shield += amount;
    }

    public void Slow(float amount)
    {
        _speed = u_speed * (1f - amount);
    }

    public void ResetSpeed()
    {
        _speed = u_speed;
    }

    public void IncreaseMaxHealth(int health)
    {
        u_health += health;
    }
}
