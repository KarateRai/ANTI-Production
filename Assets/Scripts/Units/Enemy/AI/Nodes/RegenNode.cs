using System.Collections;
using UnityEngine;

public class RegenNode : Node
{
    EnemyController controller;
    int healthToRegen;
    bool activated = false;
    public RegenNode(EnemyController controller, int healthToRegen)
    {
        this.controller = controller;
        this.healthToRegen = healthToRegen;
    }
    public override NodeState Evaluate()
    {
        if (activated == false)
        {
            Debug.Log("Casting regen");
            controller.Regen(healthToRegen, 0.1f);
            activated = true;
            return NodeState.RUNNING;
        }
        return NodeState.FAILURE;
        
    }
}
