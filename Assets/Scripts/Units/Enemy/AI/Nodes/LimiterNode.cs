using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiterNode : Node
{
    /// <summary>
    /// Sets a limiter on any node so that it can only be activated a set amount of times
    /// </summary>
    int limit;
    Node node;
    public LimiterNode(Node node, int limit)
    {
        this.node = node;
        this.limit = limit;
    }
    public override NodeState Evaluate()
    {
        if (limit > 0)
        {
            node.Evaluate();
            limit--;
            return NodeState.SUCCESS;
        }
        else {
            return NodeState.FAILURE;
        }
            
    }
}
