using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_heal : MonoBehaviour
{
    public float range;
    public int amount;
    public static Collider[] colliders;
    public LayerMask layerMask;// set in editor, on the prefab
    public GameObject healEffect; 

    void Start()
    {
        colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);
        foreach (Collider c in colliders)
        {
            if (c.tag == "Player")
            {
                c.GetComponent<PlayerController>().stats.GainHealth(amount);
                Instantiate(healEffect, c.GetComponentInParent<Transform>());
            }
        }
    }
}
