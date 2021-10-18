using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BT_AI : AI
{
    ///-------------------------AI variables-------------------------------///

    private Node topNode;
    public Transform endGoal;
    public EnemyController controller;
    private NavMeshAgent agent;
    public Weapon weapon;

    ///--------------------Targetting variables----------------------------///
    [HideInInspector]
    public List<GameObject> targetsInRange = new List<GameObject>();
    [HideInInspector]
    public GameObject closestTarget;


    public override void InitializeAI(EnemyController controller)
    {
        this.endGoal = controller.testT;
        agent = controller.GetComponent<NavMeshAgent>();
        //this.weapon = controller.weaponController.weapon;
        ContructBehaviorTree();
    }

    public override void Tick()
    {
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
        //ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        //AttackPlayerNode attackNode = new AttackPlayerNode(agent, this);
        //Sequencer attackSequence = new Sequencer(new List<Node> { findTargetsNode, closestTargetNode, attackNode });
        //topNode = new Selector(new List<Node> { attackSequence, testNode });

        MoveToGoalNode testNode = new MoveToGoalNode(agent, this);
        topNode = new Selector(new List<Node> { testNode });
    }
    
}
