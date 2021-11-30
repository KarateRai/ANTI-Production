using System.Collections;
using UnityEngine;

public class AIShieldAbility : AIAbility
{
    [SerializeField] int shieldAmount;
    public override IEnumerator Activate(EnemyController controller, Transform target)
    {
        if (controller.Stats.Shield > 0)
            yield break;
        else
        {
            controller.Stats.SetShield(shieldAmount);
        }
    }
}
