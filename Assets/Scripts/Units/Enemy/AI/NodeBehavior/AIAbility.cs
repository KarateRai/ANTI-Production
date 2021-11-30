using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAbility : ScriptableObject
{
    float abilityCD;
    protected float castTime;
    public float CoolDown => abilityCD;
    public abstract IEnumerator Activate(EnemyController controller, Transform target);
}
