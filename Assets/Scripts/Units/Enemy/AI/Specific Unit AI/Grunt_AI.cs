using System.Collections.Generic;
using UnityEngine;

public class Grunt_AI : AI
{
    public override void InitializeAI(EnemyController controller)
    {
        //this.weapon = controller.weaponController.weapon;
        ContructBehaviorTree();
        IsInit = true;
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
            Debug.LogError("TopNode returned FAILURE!");
            agent.isStopped = true;
        }
    }

    private void ContructBehaviorTree()
    {
        //MoveToGoalNode testNode = new MoveToGoalNode(agent, this);
        //FindTargetsNode findTargetsNode = new FindTargetsNode(this);
        //ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        //AttackPlayerNode attackNode = new AttackPlayerNode(agent, this);
        //Sequencer attackSequence = new Sequencer(new List<Node> { findTargetsNode, closestTargetNode, attackNode });
        //topNode = new Selector(new List<Node> { attackSequence, testNode });

        
        MoveToNode testNode = new MoveToNode(agent, this);
        topNode = new Selector(new List<Node> { testNode });
    }
    
}
