using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : Node
{
    Node node;
    NodeState state;
    bool isConditionMet = false;
    public ConditionNode(Node node)
    {
        this.node = node;
    }
    public override NodeState Evaluate()
    {
        //If condition is set to true, we will always return success. Otherwise we check if we should set it.
        if (isConditionMet)
            return NodeState.SUCCESS;
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                state = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                isConditionMet = true;
                state = NodeState.SUCCESS;
                break;
            case NodeState.FAILURE:
                state = NodeState.FAILURE;
                break;
        }
        return state;
    }
}
