using UnityEngine;
public class ClosestTargetNode : Node
{
    AI ai;
    GameObject oldGO;
    public ClosestTargetNode(AI ai)
    {
        this.ai = ai;
    }
    public override NodeState Evaluate()
    {
        if (ai.closestTarget != null)
        {
            oldGO = ai.closestTarget;
        }
        ai.closestTarget = TargetsInRange.GetClosestEnemy(ai.targetsInRange, ai.controller.transform);
        if (ai.closestTarget == null)
        {
            ai.closestTarget = oldGO;
        }
        
        return ai.closestTarget != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
