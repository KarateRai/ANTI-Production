public class KamikazeNode : Node
{
    private EnemyController controller;
    public KamikazeNode(EnemyController controller)
    {
        this.controller = controller;
    }

    public override NodeState Evaluate()
    {
        controller.UseWeapon();
        controller.Die();
        //AI will die, sooooo returning w/e
        return NodeState.SUCCESS;
    }
}
