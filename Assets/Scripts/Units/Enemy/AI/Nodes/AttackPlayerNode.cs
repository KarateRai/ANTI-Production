using UnityEngine;
using UnityEngine.AI;

public class AttackPlayerNode : Node
{
    private NavMeshAgent agent;
    private Grunt_AI ai;
    public AttackPlayerNode(NavMeshAgent agent, Grunt_AI ai)
    {
        this.agent = agent;
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
