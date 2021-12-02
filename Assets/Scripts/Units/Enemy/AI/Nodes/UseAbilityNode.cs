using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityNode : Node
{
    EnemyController controller;
    int index;
    string name;

    public UseAbilityNode(EnemyController controller ,int index)
    {
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
        string msg = controller.UseAbility(index, controller.ai.closestTarget.transform) == true ? "Ability " + name + " Success" : "Ability " + name + " Fail";
        Debug.Log(msg);
        return controller.UseAbility(index, controller.ai.closestTarget.transform) == true ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
