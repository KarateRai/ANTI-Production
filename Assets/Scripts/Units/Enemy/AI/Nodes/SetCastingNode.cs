using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCastingNode : Node
{
    EnemyController controller;
    bool stateToSet;
    public SetCastingNode(EnemyController controller, bool stateToSet)
    {
        this.controller = controller;
        this.stateToSet = stateToSet;
    }

    public override NodeState Evaluate()
    {
        if (controller.Channeling == stateToSet)
        {
            return NodeState.RUNNING;
        }
        else
        {
            controller.Channeling = stateToSet;
            return NodeState.SUCCESS;
        }
    }
}
