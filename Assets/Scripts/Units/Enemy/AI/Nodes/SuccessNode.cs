using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessNode : Node
{
    Node node;
    public SuccessNode(Node node)
    {
        this.node = node;
    }
    public SuccessNode() { }
    public override NodeState Evaluate()
    {
        node?.Evaluate();
        return NodeState.SUCCESS;
    }

}
