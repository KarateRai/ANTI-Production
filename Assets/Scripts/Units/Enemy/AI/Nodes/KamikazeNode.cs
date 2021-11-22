using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeNode : Node
{
    private EnemyController controller;
    public KamikazeNode(EnemyController controller)
    {
        this.controller = controller;
    }

    public override NodeState Evaluate()
    {
        //controller.ai.closestTarget.GetComponentInParent<PlayerController>().TakeDamage(50);
        //Use weapon for explosion
        controller.UseWeapon();
        controller.Die();
        //AI will die, sooooo returning w/e
        return NodeState.SUCCESS;
    }
}
