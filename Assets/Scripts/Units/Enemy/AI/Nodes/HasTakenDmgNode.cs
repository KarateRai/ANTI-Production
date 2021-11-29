using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTakenDmgNode : Node
{
    /// <summary>
    /// Checks if unit has taken damage after shield is broken and if so returns success
    /// </summary>
    EnemyController controller;
    int initialHealth;
    bool isSet = false;
    public HasTakenDmgNode(EnemyController controller)
    {
        this.controller = controller;
    }
    public override NodeState Evaluate()
    {
        
        if (controller.Stats.Shield <= 0)
        {
            if (isSet == false)
            {
                isSet = true;
                initialHealth = controller.Stats.Health;
            }
        }
        return controller.Stats.Health < initialHealth ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
