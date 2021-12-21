using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityNode : Node
{
    EnemyController controller;
    int index;
    string name;

    public UseAbilityNode(string name, EnemyController controller ,int index)
    {
        this.name = name;
        this.controller = controller;
        this.index = index;
    }
    public UseAbilityNode(EnemyController controller, int index, string name)
    {
        this.controller = controller;
        this.index = index;
        this.name = name;
    }

    public override NodeState Evaluate()
    {
        if (controller.ai.closestTarget == null)
            return controller.UseAbility(index, controller.transform) == true ? NodeState.SUCCESS : NodeState.FAILURE;

        return controller.UseAbility(index, controller.ai.closestTarget.transform) == true ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
