using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : UnitStats
{
   
    public EnemyStats(UnitController controller, int health, float speed) : base(controller, health, speed)
    {
    }
}
