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
        if (ai.controller.Stats.Speed + speedIncrease <= ai.controller.Stats.MaxSpeed)
        {
            ai.controller.Stats.SetSpeed(speedIncrease);
        }
        else
        {
            ai.controller.Stats.SetSpeed((int)ai.controller.Stats.MaxSpeed);
        }
        return NodeState.SUCCESS;
    }
}
