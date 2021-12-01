using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> nodes = new List<Node>();
    private string selectorName = "";

    public Selector(string selectorName, List<Node> nodes)
    {
        this.selectorName = selectorName;
        this.nodes = nodes;
    }
    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (Node node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.FAILURE:
                    break;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }

    public override string ToString()
    {
        return selectorName != "" ? selectorName : "N/A";
    }
}
