using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : ScriptableObject
{
    public abstract void Tick();

    public abstract void InitializeAI(EnemyController controller);
}
