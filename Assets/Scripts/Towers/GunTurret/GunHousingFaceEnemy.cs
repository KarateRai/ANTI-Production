using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHousingFaceEnemy : MonoBehaviour
{
    public void Target(Vector3 aTargetPosition, Vector3 aParentPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, aTargetPosition - aParentPosition);
        //SOH
        float a = aTargetPosition.x - (transform.position.x);
        float o = aTargetPosition.y - (transform.position.y);
        float h = Mathf.Sqrt((o * o) + (a * a));
        float pitch = Mathf.Asin(o / h) * Mathf.Rad2Deg;
        //float pitch = Mathf.Atan(o / a) * Mathf.Rad2Deg;
        targetRotation *= Quaternion.Euler(pitch, 0, -90);
        transform.rotation = targetRotation;
    }
}
