using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float buffDuration = 4f;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private Pickup_item pickup;
    private Renderer _rend;
    private MaterialPropertyBlock _propBlock;
    private Collider _coll;
    private bool retractPower = false;
    private void Awake()
    {
        this._rend = GetComponentInChildren<MeshRenderer>();
        this._coll = GetComponent<Collider>();
    }
    private void Start()
    {
        _rend.material.SetColor("_Color", pickup.color);
    }
    
    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Pickup(other));
    }

    private IEnumerator Pickup(Collider player)
    {
        //Ta bort, alla pickupos ska ha effect
        if (pickupEffect !=  null)
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
        }
        //Power up player


        SetAvaiable();
        retractPower = pickup.Use(player);
        yield return new WaitForSeconds(buffDuration);
        if (retractPower == true && player != null)
        {
            pickup.Remove(player);
            retractPower = false;
        }
        Destroy(gameObject);
    }

    private void SetAvaiable()
    {
        _coll.enabled = !_coll.enabled;
        _rend.enabled = !_rend.enabled;
    }
}
