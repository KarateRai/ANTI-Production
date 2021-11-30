using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThickBOI_AI : AI
{
    //Temprange, FIX IN QA
    public float toFarAwayRange = 25f;
    public float toCloseRange = 15f;
    public override void InitializeAI(EnemyController controller)
    {
        SetupWeapon();
        ContructBehaviorTree();
        IsInit = true;
    }

    private void ContructBehaviorTree()
    {
        //Phase 1, move to CPU and attack players that are within attackrange
        MoveToNode moveToCPU = new MoveToNode(agent, this); //Move to target location controlled by map
        FindTargetsInRangeNode findTargetsNode = new FindTargetsInRangeNode(controller); //Check if there are players within range
        ClosestTargetNode closestTarget = new ClosestTargetNode(this); //Set target to the closest player
        AttackPlayerNode attackNode = new AttackPlayerNode(controller); //Attack the closest player

        //If health hits < 50, start regening untill shield is broken
        HealthCheckNode healthNode = new HealthCheckNode(controller, controller.Stats.MaxHealth / 2); //Node that checks health
        Inverter invertHealthNode = new Inverter(healthNode); //Check if below instead of above
        ConditionNode healthCondition = new ConditionNode(invertHealthNode); //Condition(state) node
        HasTakenDmgNode takenDmgNode = new HasTakenDmgNode(controller); //Check if damage has been taken after shield is broken
        Inverter invertdmgTakenNode = new Inverter(takenDmgNode); //Invert, we want to know if we have NOT taken dmg
        ApplyShieldNode shieldNode = new ApplyShieldNode(controller, controller.Stats.MaxHealth / 2); //Applies shield
        LimiterNode shieldLimiterNode = new LimiterNode(shieldNode, 1); //Sets a limiter for shieldnode to ONE
        RegenNode regenHealthNode = new RegenNode(controller, controller.Stats.MaxHealth); //Regen health
        Sequencer startRegenSequence = new Sequencer(new List<Node> { invertdmgTakenNode, regenHealthNode }); //Sequence for health regen
        Selector p2Selector = new Selector(new List<Node> {shieldLimiterNode, startRegenSequence }); //Phase 2 selector
        Sequencer p2 = new Sequencer(new List<Node> { healthCondition, p2Selector }); //Sequencer to make sure condition is met first

        //Sub 50 behavior
        FindTargetsInRangeNode closeAbilityCheckNode = new FindTargetsInRangeNode(controller, toCloseRange); 
        FindTargetsInRangeNode targetsToFarAwayNode = new FindTargetsInRangeNode(controller, toFarAwayRange);
        Inverter rangeInverter = new Inverter(targetsToFarAwayNode);

        //TEMP
        SuccessNode sNode = new SuccessNode();

        Selector phaseSelector = new Selector(new List<Node> { p2, sNode });

        //BT Node
        topNode = new Selector(new List<Node> { phaseSelector });
    }
}
