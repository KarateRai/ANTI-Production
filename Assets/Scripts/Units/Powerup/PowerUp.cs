using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float buffDuration = 4f;
    [SerializeField] private float reActivationTime = 10f;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private Pickup_item pickup;
    private Renderer rend;
    private Collider coll;
    private bool retractPower = false;
    private void Awake()
    {
        this.rend = GetComponentInChildren<MeshRenderer>();
        this.coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Pickup(other));
    }

    private IEnumerator Pickup(Collider player)
    {
        Debug.Log("Collided");
        //Ta bort, alla pickupos ska ha effect
        if (pickupEffect !=  null)
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
        }
        //Power up player


        SetAvaiable();
        retractPower = pickup.Use(player);
        yield return new WaitForSeconds(buffDuration);
        if (retractPower == true)
        {
            pickup.Remove(player);
            retractPower = false;
        }
        yield return new WaitForSeconds(reActivationTime);
        SetAvaiable();
    }

    private void SetAvaiable()
    {
        coll.enabled = !coll.enabled;
        rend.enabled = !rend.enabled;
    }
}
