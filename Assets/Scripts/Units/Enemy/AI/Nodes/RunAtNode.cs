using UnityEngine;
using UnityEngine.AI;

public class RunAtNode : Node
{
    private EnemyController controller;
    private NavMeshAgent agent;
    private float shortDistance = 1.2f;

    public RunAtNode(EnemyController controller, NavMeshAgent agent)
    {
        this.controller = controller;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(controller.ai.closestTarget.transform.position, controller.transform.position);
        
        agent.SetDestination(controller.ai.closestTarget.transform.position);
        
        if (distance > shortDistance)
        {
            agent.isStopped = false;
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
