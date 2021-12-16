using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public WeaponController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller.SetShootingPos();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Fire();
    }
}
