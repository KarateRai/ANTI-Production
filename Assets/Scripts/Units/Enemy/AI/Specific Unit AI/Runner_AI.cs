using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner_AI : AI
{
    public override void InitializeAI(EnemyController controller)
    {
        SetupWeapon();
        ContructBehaviorTree();
        IsInit = true;
        agent.speed = controller.Stats.Speed;
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
        FindTargetsInRangeNode findTargetNode = new FindTargetsInRangeNode(controller);
        ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        IncreaseSpeedNode runFasterNode = new IncreaseSpeedNode(this, 5);
        RunAtNode runAtNode = new RunAtNode(controller, agent);
        KamikazeNode kamikazeNode = new KamikazeNode(controller);
        Sequencer runAtPlayer = new Sequencer(new List<Node> { findTargetNode, closestTargetNode, runFasterNode, runAtNode, kamikazeNode });
        topNode = new Selector(new List<Node> { runAtPlayer, moveToCPU });
    }
}
