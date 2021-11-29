using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : MonoBehaviour
{
    public GameObject myStand;
    public GameObject myGunHousing;
    public Animator myAnimator;
    public float myRange = 10;

    public GameObject target;
    float countDown = 2.0f;
    private Collider myCollider;
    private List<GameObject> enemyList;
    private WeaponController myWC;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
        myWC = GetComponent<WeaponController>();
        myCollider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        //enemyList = TargetsInRange.FindTargets(360, myRange, transform, myCollider, myWC.TargetLayer);
        //target = TargetsInRange.GetClosestEnemy(enemyList, transform);
        if (countDown <= 0f && target != null)
        {
            countDown = 2.0f;
            Shoot();
        }

        //TurnToEnemy
        Quaternion standTargetRotation = Quaternion.LookRotation(Vector3.up, target.transform.position - transform.position);
        standTargetRotation *= Quaternion.Euler(0, 0, -90);
        myStand.transform.rotation = standTargetRotation;

        //GunHousing
        Quaternion housingTargetRotation = Quaternion.LookRotation(Vector3.up, (target.transform.position - transform.position) + new Vector3(0, 0.5f, 0));
        //SOH
        float a = target.transform.position.x - (transform.position.x + myGunHousing.transform.position.x);
        float o = target.transform.position.y - (transform.position.y + myGunHousing.transform.position.y);
        float h = Mathf.Sqrt((o * o) + (a * a));
        float pitch = Mathf.Asin(o / h) * Mathf.Rad2Deg;
        Debug.Log(pitch);
        //float pitch = Mathf.Atan(o / a) * Mathf.Rad2Deg;
        housingTargetRotation *= Quaternion.Euler(pitch, 0, -90);
        myGunHousing.transform.rotation = housingTargetRotation;
    }

    void Shoot()
    {
        myAnimator.Play("Scene");
        myWC.Fire();
    }
}
