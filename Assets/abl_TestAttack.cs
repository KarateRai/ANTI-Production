using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abl_TestAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward *0.1f;


        //if hit
        //enemy within radius .takedamage(damage)
        //RemoveInstance 
    }
    private void RemoveInstance()
    {
        Destroy(gameObject);
    }
}
