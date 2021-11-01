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
        if (controller.weaponController.TargetLayer == 0 || controller.weaponController.IgnoreLayer == 0)
        {
            Debug.Log("Layer arrays wasn't filled out");
            return NodeState.FAILURE;
        }


        controller.ai.targetsInRange?.Clear();
        controller.ai.targetsInRange =
            TargetsInRange.FindTargets(360f, controller.ai.range, controller.transform, controller.GetComponent<Collider>(),
            controller.weaponController.TargetLayer, controller.weaponController.IgnoreLayer);
        return controller.ai.targetsInRange != null ? NodeState.SUCCESS : NodeState.FAILURE;

    }

    
}
