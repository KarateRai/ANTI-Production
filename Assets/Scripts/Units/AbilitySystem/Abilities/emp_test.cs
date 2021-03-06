using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emp_test : MonoBehaviour
{
    public float range;
    public int damage;
    public static Collider[] colliders;
    public LayerMask layerMask;// set in editor, on the prefab
    // Start is called before the first frame update
    void Start()
    {
        colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);
        foreach (Collider c in colliders)
        {
            if (c.tag == "AI")
            {
                c.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
