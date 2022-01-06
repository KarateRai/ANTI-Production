using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThickBOI_AI : AI
{
    public bool hasTakenDmg = false;
    public ParticleSystem shieldEffect;
    public float toFarAwayRange = 15f;
    public float toCloseRange = 7f;
    public override void InitializeAI(EnemyController controller)
    {
        SetupParticleWeapon();
        ContructBehaviorTree();
        IsInit = true;
        controller.Stats.SetShieldEffect(shieldEffect);
    }

    private void ContructBehaviorTree()
    {
        //Phase 1, move to CPU and attack players that are within attackrange
        MoveToNode moveToCPU = new MoveToNode(agent, this); //Move to target location controlled by map
        FindTargetsInRangeNode findTargetsNode = new FindTargetsInRangeNode(controller); //Check if there are players within range
        ClosestTargetNode closestTargetNode = new ClosestTargetNode(this); //Set target to the closest player
        AttackPlayerNode attackNode = new AttackPlayerNode(controller); //Attack the closest player
        Sequencer attackSequence = new Sequencer(new List<Node> { findTargetsNode, closestTargetNode, attackNode });
        Selector p1 = new Selector(new List<Node>() {attackSequence, moveToCPU });

        //If health hits < 50, start regening untill shield is broken
        HealthCheckNode healthNode = new HealthCheckNode(controller, controller.Stats.MaxHealth / 2); //Node that checks health
        Inverter invertHealthNode = new Inverter(healthNode); //Check if below instead of above
        ConditionNode healthCondition = new ConditionNode(invertHealthNode); //Condition(state) node
        ShieldIsBrokenNode takenDmgNode = new ShieldIsBrokenNode(controller); //Check if damage has been taken after shield is broken
        Inverter invertdmgTakenNode = new Inverter(takenDmgNode); //Invert, we want to know if we have NOT taken dmg
        ApplyShieldNode shieldNode = new ApplyShieldNode(controller, controller.Stats.MaxHealth / 2); //Applies shield
        LimiterNode shieldLimiterNode = new LimiterNode(shieldNode, 1); //Sets a limiter for shieldnode to ONE
        RegenNode regenHealthNode = new RegenNode(controller, controller.Stats.MaxHealth); //Regen health
        Sequencer startRegenSequence = new Sequencer(new List<Node> { invertdmgTakenNode, regenHealthNode }); //Sequence for health regen
        Selector p2Selector = new Selector(new List<Node> {shieldLimiterNode, startRegenSequence}); //Phase 2 selector
        Sequencer p2 = new Sequencer(new List<Node> { healthCondition, p2Selector }); //Sequencer to make sure condition is met first

        //Sub 50 behavior
        FindTargetsInRangeNode closeAbilityCheckNode = new FindTargetsInRangeNode(controller, toCloseRange); //Check if targets are to close
        FindTargetsInRangeNode targetsToFarAwayNode = new FindTargetsInRangeNode(controller, toFarAwayRange);
        Inverter rangeInverter = new Inverter(targetsToFarAwayNode); //Invert rangecheck so we look if no one is to close
        TargetCounterNode countTargetsTooFarAway = new TargetCounterNode(controller, 0.5f); //If 50% of the players are to far away, use shield
        UseAbilityNode abilityOneNode = new UseAbilityNode("A1", controller, 0); //Shield
        UseAbilityNode abilityTwoNode = new UseAbilityNode("A2", controller, 1); //ChargeAbility
        ConditionNode hasTakenDamageCondition = new ConditionNode(takenDmgNode);

        Sequencer abilityOneSequencer = new Sequencer(new List<Node>() {closeAbilityCheckNode, closestTargetNode, abilityTwoNode });
        Sequencer abilityTwoSequencer = new Sequencer(new List<Node>() {rangeInverter, abilityOneNode });

        Selector p3Selector = new Selector("p3 selector", new List<Node> { abilityOneSequencer, abilityTwoSequencer, attackSequence, moveToCPU });
        Sequencer p3 = new Sequencer(new List<Node> { healthCondition, takenDmgNode, p3Selector });
                
        //BT Node
        topNode = new Selector(new List<Node> { p3, p2, p1 });
    }
}
