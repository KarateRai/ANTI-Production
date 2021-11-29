using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //Take damage position (if we want it to differ from object
    public Transform takeDamagePosition;

    public abstract void TakeDamage(int amount);
    public abstract void GainHealth(int amount);
    public abstract void AffectSpeed(int amount);

    public virtual void Die()
    {
        isDead = true;
        //Earn gold/resources etc for killing enemy here
        Debug.Log(gameObject.name + " has died.");
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        gameObject.SetActive(false);
    }
    public bool IsDead()
    {
        return isDead;
    }

    public abstract IEnumerator Regen(int amountToRegen, float regenSpeed);
}
