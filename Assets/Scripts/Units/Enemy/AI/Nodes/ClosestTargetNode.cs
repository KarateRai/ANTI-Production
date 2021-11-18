using UnityEngine;
public class ClosestTargetNode : Node
{
    AI ai;

    public ClosestTargetNode(AI ai)
    {
        this.ai = ai;
    }
    public override NodeState Evaluate()
    {
        ai.closestTarget = TargetsInRange.GetClosestEnemy(ai.targetsInRange, ai.controller.transform);
        return ai.closestTarget != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
