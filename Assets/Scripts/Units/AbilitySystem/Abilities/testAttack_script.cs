using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAttack_script : MonoBehaviour
{
    ///----------------Attributes(Set in ability)--------------------///
    public float damage;
    public float range;

    ///--------------------Targets to Hit/Ignore----------------------------///
    private GameObject targetObj;
    private Transform targetPosition;
    public LayerMask targetLayer;
    public LayerMask ignoreLayer;

    public GameObject impactEffect;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 4f);
    }
    private void FixedUpdate()
    {
        this.transform.position += transform.forward * 0.5f;
    }

    void HitTarget()
    {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        GameObject parent = GameObject.Find("InstantiatedObjects");
        effectInstance.transform.SetParent(parent.transform);
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in cols)
        {
            if (col.tag == "AI")
            { 
                EnemyController script = col.GetComponent<EnemyController>();
                script.TakeDamage((int)damage); 
            }
        }

        RemoveInstance();
    }
    private void RemoveInstance()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "AI")
        {
            HitTarget();
        }
        
        if (targetLayer == (targetLayer | (1 << collider.gameObject.layer)))
        {
            HitTarget();
        }
        else if (ignoreLayer == (ignoreLayer | (1 << collider.gameObject.layer)))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
        //GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        
    }
}
