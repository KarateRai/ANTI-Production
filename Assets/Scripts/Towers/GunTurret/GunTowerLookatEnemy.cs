using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTowerLookatEnemy : MonoBehaviour
{
    public void Target(Vector3 aTargetPosition, Vector3 aParentPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, aTargetPosition - aParentPosition);
        targetRotation *= Quaternion.Euler(0, 0, -90);
        transform.rotation = targetRotation;
    }
}
