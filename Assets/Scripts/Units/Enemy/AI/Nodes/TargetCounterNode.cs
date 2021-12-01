using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCounterNode : Node
{
    EnemyController controller;
    float procentOfTargetsCounted;
 
    public TargetCounterNode(EnemyController controller, float percentOfTargetsCounted)
    {
        this.controller = controller;
        this.procentOfTargetsCounted = percentOfTargetsCounted;
    }
    public override NodeState Evaluate()
    {
        if (GameManager.instance == null)
        {
            Debug.LogWarning("No Gamemanager");
            return NodeState.FAILURE;
        }
        if (GameManager.instance.gameLogic == null || controller.ai.targetsInRange == null)
            return NodeState.FAILURE;
        else if (GameManager.instance.gameLogic.alivePlayer.Count == 1)
            return NodeState.SUCCESS;
        else
            return controller.ai.targetsInRange.Count >= (procentOfTargetsCounted * GameManager.instance.gameLogic.alivePlayer.Count)
            ? NodeState.SUCCESS : NodeState.FAILURE;
        
         
    }
}
