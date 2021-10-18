using UnityEngine;

public class FindTargetsNode : Node
{
    BT_AI ai;
    public FindTargetsNode(BT_AI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (ai.weapon.targetLayer == 0 || ai.weapon.ignoreLayer == 0)
        {
            Debug.Log("Layer arrays wasn't filled out");
            return NodeState.FAILURE;
        }


        ai.targetsInRange?.Clear();
        ai.targetsInRange =
            TargetsInRange.FindTargets(360f, ai.weapon.range, ai.controller.transform, ai.controller.GetComponent<Collider>(),
            ai.weapon.targetLayer, ai.weapon.ignoreLayer);
        return ai.targetsInRange != null ? NodeState.SUCCESS : NodeState.FAILURE;

    }

    
}
