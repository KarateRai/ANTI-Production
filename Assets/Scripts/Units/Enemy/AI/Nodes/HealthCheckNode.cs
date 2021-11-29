using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCheckNode : Node
{
    //This node could be remodeled so that you send in an integer that you compare to and if its bigger you send back success.
    //Inverter for flipper results

    /// <summary>
    /// Node that checks if health is above/below threshold.
    /// </summary>

    private EnemyController controller;
    private int threshold;
    public HealthCheckNode(EnemyController controller, int threshold)
    {
        this.controller = controller;
        this.threshold = threshold;
    }
    public override NodeState Evaluate()
    {
        return controller.Stats.Health > threshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
