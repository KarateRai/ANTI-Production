using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Node
{
    protected List<Node> nodes = new List<Node>();

    public Sequencer(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    //Note, might be wrong
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
                    break;
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
