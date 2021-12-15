using System.Collections.Generic;
using UnityEngine;

public class Grunt_AI : AI
{
    public override void InitializeAI(EnemyController controller)
    {
        SetupParticleWeapon();
        ContructBehaviorTree();
        IsInit = true;
    }
   
    private void ContructBehaviorTree()
    {
        MoveToNode moveToEndNode = new MoveToNode(agent, this);
        
        FindTargetsInRangeNode findTargetsNode = new FindTargetsInRangeNode(controller);
        ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        AttackPlayerNode attackNode = new AttackPlayerNode(controller);

        Sequencer attackSequence = new Sequencer(new List<Node> { findTargetsNode, closestTargetNode, attackNode });
        topNode = new Selector(new List<Node> { attackSequence, moveToEndNode });
    }
    
}
