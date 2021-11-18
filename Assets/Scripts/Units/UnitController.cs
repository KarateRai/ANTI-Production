using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UnitController : MonoBehaviour
{
    //Controller for unit weapon
    public WeaponController weaponController;

    //Health variables
    [SerializeField] protected GameObject deathEffect;
    public Image healthBar;
    protected bool isDead;

    //Take damage position (if we want it to differ from object
    public Transform takeDamagePosition;

    public abstract void TakeDamage(int amount);

    public abstract void GainHealth(int amount);

    public virtual void Die()
    {
        isDead = true;
        //Earn gold/resources etc for killing enemy here

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        gameObject.SetActive(false);
    }
}
