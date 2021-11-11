using UnityEngine;
using UnityEngine.AI;

public class RunAtNode : Node
{
    private NavMeshAgent agent;
    private PlayerController player;

    public RunAtNode(NavMeshAgent agent, PlayerController player)
    {
        this.agent = agent;
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(player.transform.position, agent.transform.position);
        
        
        if (distance > 5.5f)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            return NodeState.RUNNING;
        }
        else if (distance < 5.5f && distance > 1f)
        {
            agent.speed += 5;
            return NodeState.RUNNING;
        }
        else
        {
            //Destroy AI GO
            player.TakeDamage(50);
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
