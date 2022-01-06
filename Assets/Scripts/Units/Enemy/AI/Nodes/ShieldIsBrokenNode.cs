using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldIsBrokenNode : Node
{
    /// <summary>
    /// Checks if unit has taken damage after shield is broken and if so returns success
    /// </summary>
    EnemyController controller;
    int initialHealth;
    bool isSet = false;
    public ShieldIsBrokenNode(EnemyController controller)
    {
        this.controller = controller;
    }
    public override NodeState Evaluate()
    {
        
        if (controller.Stats.Shield > 0)
        {
            if (isSet == false)
            {
                isSet = true;
            }
        }
        if (isSet == true)
        {
            if (controller.Stats.Shield < 0)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
        return NodeState.FAILURE;
    }
}
