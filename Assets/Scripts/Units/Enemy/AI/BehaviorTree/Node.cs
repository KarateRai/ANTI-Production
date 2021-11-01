using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }

    protected NodeState state;
    public NodeState State { get { return state; } }

    public abstract NodeState Evaluate();
}
