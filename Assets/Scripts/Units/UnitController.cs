using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UnitController : MonoBehaviour
{
    //Controller for unit weapon
    public WeaponController weaponController;
    
    public bool Channeling { get; set; }

    //Health variables
    [SerializeField] protected GameObject deathEffect;
    public Image healthBar;
    protected bool isDead;
    protected bool isGameOver;
    protected UnityAction onDeath;
    //Take damage position (if we want it to differ from object
    public Transform takeDamagePosition;

    public abstract void TakeDamage(int amount);
    public abstract void GainHealth(int amount);
    public abstract void AffectSpeed(int amount);

    private void Awake()
    {
        GlobalEvents.instance.onGameOver += GameOver;
    }
    protected void GameOver()
    {
        isGameOver = true;
    }
    public virtual void Die()
    {
        isDead = true;
        onDeath?.Invoke();
        //Earn gold/resources etc for killing enemy here
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 30f);

        gameObject.SetActive(false);
    }
    public bool IsDead()
    {
        return isDead;
    }

    public abstract void Regen(int amountToRegen, float regenSpeed);
    private void OnDestroy()
    {
        GlobalEvents.instance.onGameOver -= GameOver;
    }
}
