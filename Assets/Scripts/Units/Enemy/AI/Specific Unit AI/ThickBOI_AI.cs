using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThickBOI_AI : AI
{
    public override void InitializeAI(EnemyController controller)
    {
        ContructBehaviorTree();
        IsInit = true;
        //agent.speed = controller.Stats.Speed;
    }

    private void ContructBehaviorTree()
    {
        //Find random player, set as target. KAMIKAZEEEEEE!
        MoveToNode moveToCPU = new MoveToNode(agent, this);
        AttackPlayerNode attackNode = new AttackPlayerNode(this);

        //If health hits < 50, start regening untill shield is broken
        HealthCheckNode healthNode = new HealthCheckNode(controller, controller.Stats.MaxHealth / 2);
        Inverter invertHealthNode = new Inverter(healthNode);
        ConditionNode healthCondition = new ConditionNode(invertHealthNode);
        HasTakenDmgNode takenDmgNode = new HasTakenDmgNode(controller);
        Inverter invertdmgTakenNode = new Inverter(takenDmgNode);
        ApplyShieldNode shieldNode = new ApplyShieldNode(controller, controller.Stats.MaxHealth / 2);
        LimiterNode shieldLimiterNode = new LimiterNode(shieldNode, 1);
        RegenNode regenHealthNode = new RegenNode(controller, controller.Stats.MaxHealth);
        Sequencer startRegenSequence = new Sequencer(new List<Node> { invertdmgTakenNode, regenHealthNode });
        Selector p2Selector = new Selector(new List<Node> {shieldLimiterNode, startRegenSequence });
        Sequencer p2 = new Sequencer(new List<Node> { healthCondition, p2Selector });

        //TEMP
        SuccessNode sNode = new SuccessNode();

        Selector phaseSelector = new Selector(new List<Node> { p2, sNode });

        //BT Node
        topNode = new Selector(new List<Node> { phaseSelector });
    }
}
