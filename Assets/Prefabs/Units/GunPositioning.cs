using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPositioning : MonoBehaviour
{
    public Transform lefthand;
    public Transform righthand;

    void FixedUpdate()
    {
        transform.position = ( lefthand.transform.position + righthand.transform.position) / 2;
        transform.localPosition -= new Vector3(0, 0.73f, 0);
    }
}
