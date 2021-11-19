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
        //float distance = Vector3.Distance(ai.controller.toObjPosition.transform.position, agent.transform.position);
        float distance = Vector3.Distance(ai.controller.TEMPPOS.transform.position, agent.transform.position);


        if (distance > 1.8f)
        {
            agent.isStopped = false;
            agent.SetDestination(ai.controller.TEMPPOS.transform.position);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
            //if (GameManager.Instance.stagemanager != null)
            //{
            //    GameManager.Instance.stagemanager.AddCorruption(25); //Testing value
            //}
            //ai.stats.HorribleDeath();
            //return NodeState.SUCCESS;
        }
    }
}
