using UnityEngine;
using UnityEngine.AI;

public class AttackPlayerNode : Node
{
    EnemyController controller;
    public AttackPlayerNode(EnemyController controller)
    {
        this.controller = controller;
    }
    public override NodeState Evaluate()
    {
        return Attack(controller.ai.closestTarget) == true ? NodeState.SUCCESS : NodeState.FAILURE;
    }
    private bool Attack(GameObject obj)
    {
        if (controller.ai.closestTarget == null || controller.ai.closestTarget.activeSelf == false)
        {
            return false;
        }
        else
        {
            controller.UseWeapon(obj);
            return true;
        }
        
    }
}
