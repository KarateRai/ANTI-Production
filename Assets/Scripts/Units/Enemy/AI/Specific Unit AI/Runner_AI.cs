using System.Collections.Generic;
using UnityEngine;

public class Runner_AI : AI
{
    public int speedIncrease = 5;
    [SerializeField] ParticleSystem fuseEffect;
    public override void InitializeAI(EnemyController controller)
    {
        SetupWeapon();
        ContructBehaviorTree();
        IsInit = true;
        agent.speed = controller.Stats.Speed;
        fuseEffect = Instantiate(fuseEffect);
        fuseEffect.transform.parent = controller.transform;
    }

    private void ContructBehaviorTree()
    {
        
        //Find random player, set as target. KAMIKAZEEEEEE!
        MoveToNode moveToCPU = new MoveToNode(agent, this);

        FindTargetsInRangeNode findTargetNode = new FindTargetsInRangeNode(controller);
        ClosestTargetNode closestTargetNode = new ClosestTargetNode(this);
        IncreaseSpeedNode runFasterNode = new IncreaseSpeedNode(this, speedIncrease);
        RunAtNode runAtNode = new RunAtNode(controller, agent, fuseEffect);
        KamikazeNode kamikazeNode = new KamikazeNode(controller);
        ConditionNode targetFoundNode = new ConditionNode(closestTargetNode);
        Inverter hasNotFoundTargetNode = new Inverter(targetFoundNode);
        Sequencer searchForPlayer = new Sequencer(new List<Node> { findTargetNode, closestTargetNode });
        Sequencer moveToCPUSequencer = new Sequencer(new List<Node> { hasNotFoundTargetNode, moveToCPU });
        Sequencer kamikazeSequencer = new Sequencer(new List<Node> { targetFoundNode, runFasterNode, runAtNode, kamikazeNode });

        topNode = new Selector(new List<Node> { kamikazeSequencer, searchForPlayer, moveToCPUSequencer });
    }
}
