using System.Collections;
using UnityEngine;

public class RegenNode : Node
{
    EnemyController controller;
    int healthToRegen;
    public RegenNode(EnemyController controller, int healthToRegen)
    {
        this.controller = controller;
        this.healthToRegen = healthToRegen;
    }
    public override NodeState Evaluate()
    {
        Debug.Log("Inside REGEN");
        if (healthToRegen > 0)
        {
            controller.Regen(healthToRegen, 0.1f);
            return NodeState.RUNNING;
        }
        return NodeState.FAILURE;
        
    }
}
