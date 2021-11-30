using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityNode : Node
{
    EnemyController controller;
    int index;

    public UseAbilityNode(EnemyController controller ,int index)
    {
        this.controller = controller;
        this.index = index;
    }
    
    public override NodeState Evaluate()
    {
        return controller.UseAbility(index, controller.ai.closestTarget.transform) == true ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
