using System.Collections;
using UnityEngine;

public abstract class AIAbility : ScriptableObject
{
    public float abilityCD = 5;
    public float castTime;
    public float CoolDown => abilityCD;
    public abstract IEnumerator Activate(EnemyController controller, Transform target);
}
