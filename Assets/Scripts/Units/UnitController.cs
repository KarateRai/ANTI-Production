using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController : MonoBehaviour
{
    //Controller for unit weapon
    public WeaponController weaponController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void TakeDamage(int amount);

    public abstract void GainHealth(int amount);

    protected abstract void Die();
}
