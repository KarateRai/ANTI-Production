
public class ClosestTargetNode : Node
{
    Grunt_AI ai;

    public ClosestTargetNode(Grunt_AI ai)
    {
        this.ai = ai;
    }
    public override NodeState Evaluate()
    {
        ai.closestTarget = TargetsInRange.GetClosestEnemy(ai.targetsInRange, ai.controller.transform);
        return ai.closestTarget != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
