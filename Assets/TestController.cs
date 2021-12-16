using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : UnitController
{
    UnitStats stats;
    public override void AffectSpeed(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void GainHealth(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void Regen(int amountToRegen, float regenSpeed)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int amount)
    {
        Debug.Log("I took " + amount + " damage!");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
