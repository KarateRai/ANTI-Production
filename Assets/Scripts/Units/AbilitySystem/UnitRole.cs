using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRole : ScriptableObject
{
    public enum UnitType
    {
        Player,
        Enemy,
        Tower
    }
    public UnitType unitType;
    public Ability[] abilities;
}
