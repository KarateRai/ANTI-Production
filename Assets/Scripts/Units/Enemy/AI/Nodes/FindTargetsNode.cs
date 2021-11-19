using UnityEngine;

public class FindTargetsNode : Node
{
    EnemyController controller;
    public FindTargetsNode(EnemyController controller)
    {
        this.controller = controller;
    }

    public override NodeState Evaluate()
    {
        if (controller.weaponController.TargetLayer == 0)
        {
            Debug.Log("Layer wasn't filled out");
            return NodeState.FAILURE;
        }


        controller.ai.targetsInRange?.Clear();
        controller.ai.targetsInRange =
            TargetsInRange.FindTargets(360f, controller.ai.range, controller.transform, controller.GetComponent<Collider>(),
            controller.weaponController.TargetLayer);
        return controller.ai.targetsInRange != null ? NodeState.SUCCESS : NodeState.FAILURE;

    }

    
}
