using UnityEngine;
using UnityEngine.AI;

public class AttackPlayerNode : Node
{
    private AI ai;
    public AttackPlayerNode(AI ai)
    {
        this.ai = ai;
    }
    public override NodeState Evaluate()
    {
        return Attack(ai.closestTarget) == true ? NodeState.SUCCESS : NodeState.FAILURE;
    }
    private bool Attack(GameObject obj)
    {
        if (ai.closestTarget == null)
        {
            return false;
        }
        else
        {
            //ai.unitWeapon.Attack(obj);
            return true;
        }
        
    }
}
