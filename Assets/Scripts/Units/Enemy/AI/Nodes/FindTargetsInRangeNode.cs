using UnityEngine;

public class FindTargetsInRangeNode : Node
{
    EnemyController controller;
    float range;
    public FindTargetsInRangeNode(EnemyController controller)
    {
        this.controller = controller;
        range = controller.ai.range;
    }
    public FindTargetsInRangeNode(EnemyController controller, float range)
    {
        this.controller = controller;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
       
        if (controller.weaponController.TargetLayer == 0)
        {
            return NodeState.FAILURE;
        }


        controller.ai.targetsInRange?.Clear();
        controller.ai.targetsInRange =
            TargetsInRange.FindTargets(360f, range, controller.transform, controller.GetComponent<Collider>(),
            controller.weaponController.TargetLayer);
        return controller.ai.targetsInRange != null ? NodeState.SUCCESS : NodeState.FAILURE;

    }
}
