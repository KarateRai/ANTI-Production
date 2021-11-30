using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : MonoBehaviour
{
    public GameObject myGunObject;
    public Animator myAnimator;
    public float myRange = 10;

    private GameObject target;
    private float countDown = 2.0f;
    private Collider myCollider;
    private List<GameObject> enemyList;
    private WeaponController myWC;

    void Start()
    {
        enemyList = new List<GameObject>();
        myWC = GetComponent<WeaponController>();
        myCollider = gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        enemyList = TargetsInRange.FindTargets(360, myRange, transform, myCollider, myWC.TargetLayer);
        target = TargetsInRange.GetClosestEnemy(enemyList, transform);
        if (countDown <= 0f && target != null)
        {
            countDown = 2.0f;
            Shoot();
        }

        //Enemy Tracking
        Quaternion targetRotation = Quaternion.LookRotation((target.transform.position - (transform.position + myGunObject.transform.position / 2)), Vector3.up);
        //Find angle of pitch (up/down rotation) with trig equation sin(theta) = Opposite / Hypothenuse
        float a = (target.transform.position - myGunObject.transform.position).magnitude; //Base
        float o = target.transform.position.y - (myGunObject.transform.position.y); // Height
        float h = Mathf.Sqrt((o * o) + (a * a)); //a2 + b2 = c2
        float pitch = Mathf.Asin(o / h) * Mathf.Rad2Deg;
        targetRotation *= Quaternion.Euler(0, 90, 0);
        myGunObject.transform.rotation = targetRotation;
    }

    void Shoot()
    {
        myAnimator.Play("Shoot");
        myWC.Fire();
    }
}
