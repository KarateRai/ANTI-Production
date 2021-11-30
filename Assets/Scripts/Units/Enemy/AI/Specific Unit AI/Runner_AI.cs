using System.Collections.Generic;

public class Runner_AI : AI
{
    public int speedIncrease = 5;
    public override void InitializeAI(EnemyController controller)
    {
        SetupWeapon();
        ContructBehaviorTree();
        IsInit = true;
        agent.speed = controller.Stats.Speed;
    }

    private void ContructBehaviorTree()
    {
        
        //Find random player, set as target. KAMIKAZEEEEEE!
        MoveToNode moveToCPU = new MoveToNode(agent, this);

        FindTargetsInRangeNode findTargetNode = new FindTargetsInRangeNode(controller);
        ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        IncreaseSpeedNode runFasterNode = new IncreaseSpeedNode(this, speedIncrease);
        RunAtNode runAtNode = new RunAtNode(controller, agent);
        KamikazeNode kamikazeNode = new KamikazeNode(controller);
        Sequencer runAtPlayer = new Sequencer(new List<Node> { findTargetNode, closestTargetNode, runFasterNode, runAtNode, kamikazeNode });

        topNode = new Selector(new List<Node> { runAtPlayer, moveToCPU });
    }
}
