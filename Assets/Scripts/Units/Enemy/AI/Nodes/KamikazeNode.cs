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
        if (controller == null)
        {
            return NodeState.FAILURE;
        }
        Debug.Log("Using weapon");
        controller.UseWeapon();
        GameManager.instance.cameraDirector.ShakeCamera(CameraDirector.ShakeIntensity.SMALL);
        controller.Die();
        //AI will die, sooooo returning w/e
        return NodeState.SUCCESS;
    }
}
