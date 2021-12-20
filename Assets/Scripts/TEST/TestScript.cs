using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public WeaponController controller;
    public LineRenderer line;
    public GameObject shootingPos;
    public LayerMask mask;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        controller.SetShootingPos();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Fire();
        RaycastHit hit;
        if (Physics.Raycast(shootingPos.transform.position, transform.forward, out hit, 20f, mask))
        {
            //Direction and distance
            Vector3 dir = (hit.point - shootingPos.transform.position).normalized;
            float dist = (hit.point.sqrMagnitude - shootingPos.transform.position.sqrMagnitude);
            line.SetPosition(1, shootingPos.transform.forward * dist);
        }
        else
            line.SetPosition(1, shootingPos.transform.forward * offset);
    }
}
