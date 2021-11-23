using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseHousing : MonoBehaviour
{
    public void Target(Vector3 aTargetPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, aTargetPosition - GetComponentInParent<Transform>().position);
        targetRotation *= Quaternion.Euler(0, 0, -180);
        transform.rotation = targetRotation;
    }
}
