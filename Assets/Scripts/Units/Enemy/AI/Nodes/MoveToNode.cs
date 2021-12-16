using UnityEngine;
using UnityEngine.AI;

public class MoveToNode : Node
{
    private NavMeshAgent agent;
    private AI ai;

    public MoveToNode(NavMeshAgent agent, AI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (ai.controller.toObjPosition == null)
        {
            return NodeState.FAILURE;
        }
        float distance = Vector3.Distance(ai.controller.toObjPosition.transform.position, agent.transform.position);
        //float distance = Vector3.Distance(ai.controller.TEMPPOS.transform.position, agent.transform.position);

        if (distance > 1.8f)
        {
            agent.SetDestination(ai.controller.toObjPosition.transform.position);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
