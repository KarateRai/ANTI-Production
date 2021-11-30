using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup_item : ScriptableObject
{
    public Color color;
    public abstract bool Use(Collider player);
    public abstract void Remove(Collider player);
}
