using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShieldNode : Node
{
    EnemyController controller;
    int shieldAmount;
    public ApplyShieldNode(EnemyController controller, int shieldAmount)
    {
        this.controller = controller;
        this.shieldAmount = shieldAmount;
    }
    public override NodeState Evaluate()
    {
        controller.Stats.SetShield(shieldAmount);
        return NodeState.SUCCESS;
    }
}
