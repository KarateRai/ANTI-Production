using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner_AI : AI
{
    public override void InitializeAI(EnemyController controller)
    {
        //this.weapon = controller.weaponController.weapon;
        ContructBehaviorTree();
        IsInit = true;
        agent.speed = controller.Stats.Speed;
    }

    public override void Tick()
    {
        if (!IsInit)
        {
            InitializeAI(controller);
            return;
        }
        if (agent.speed != controller.Stats.Speed)
        {
            agent.speed = controller.Stats.Speed;
        }

        topNode.Evaluate();
        if (topNode.State == Node.NodeState.FAILURE)
        {
            agent.isStopped = true;
        }
    }

    private void ContructBehaviorTree()
    {
        //MoveToGoalNode testNode = new MoveToGoalNode(agent, this);
        //FindTargetsNode findTargetsNode = new FindTargetsNode(this);
        //AttackPlayerNode attackNode = new AttackPlayerNode(agent, this);
        //Sequencer attackSequence = new Sequencer(new List<Node> { findTargetsNode, closestTargetNode, attackNode });
        //topNode = new Selector(new List<Node> { attackSequence, testNode });

        //Find random player, set as target. KAMIKAZEEEEEE!
        MoveToNode moveToCPU = new MoveToNode(agent, this);
        FindTargetsNode findTargetNode = new FindTargetsNode(controller);
        ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        IncreaseSpeedNode runFasterNode = new IncreaseSpeedNode(this, 5);
        RunAtNode runAtNode = new RunAtNode(controller, agent);
        Sequencer runAtPlayer = new Sequencer(new List<Node> { findTargetNode, closestTargetNode, runFasterNode, runAtNode });
        topNode = new Selector(new List<Node> { runAtPlayer, moveToCPU });
    }
}
