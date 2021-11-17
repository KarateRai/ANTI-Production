using UnityEngine;
using UnityEngine.AI;

public class IncreaseSpeedNode : Node
{
    private AI ai;
    private int speedIncrease;

    public IncreaseSpeedNode(AI ai, int speedIncrease)
    {
        this.ai = ai;
        this.speedIncrease = speedIncrease;
    }

    public override NodeState Evaluate()
    {
        ai.controller.Stats.GainSpeed(10);
        return NodeState.SUCCESS;
    }
}
