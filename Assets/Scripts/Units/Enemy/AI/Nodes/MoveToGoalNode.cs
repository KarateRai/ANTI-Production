using UnityEngine;
using UnityEngine.AI;

public class MoveToGoalNode : Node
{
    private NavMeshAgent agent;
    private BT_AI ai;

    public MoveToGoalNode(NavMeshAgent agent, BT_AI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(ai.endGoal.position, agent.transform.position);
        if (distance > 1.8f)
        {
            agent.isStopped = false;
            agent.SetDestination(ai.endGoal.position);
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
