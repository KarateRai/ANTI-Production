using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttenuationObject : MonoBehaviour
{
    public Transform cam;
    public Transform camTarget; 

    void Update()
    {
        float dist = Vector3.Distance(cam.position, camTarget.position)-25;
        Vector3 newPos = cam.position + cam.forward * dist;
        transform.position = newPos;
    }
}
