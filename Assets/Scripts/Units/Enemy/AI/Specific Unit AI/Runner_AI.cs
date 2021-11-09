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
        agent.speed += 20f;
    }

    public override void Tick()
    {
        if (!IsInit)
        {
            InitializeAI(controller);
            Debug.Log("Initialized");
            return;
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

        PlayerController player = FindObjectOfType<PlayerController>();
        //Find random player, set as target. KAMIKAZEEEEEE!
        RunAtNode runAtNode = new RunAtNode(agent, player);
        topNode = new Selector(new List<Node> { runAtNode });
    }
}
