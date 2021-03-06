using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Tower
{
    float activationDistance = 2.0f;
    private bool isCountingDown = false;

    protected override void Start()
    {
        enemyList = new List<GameObject>();
        wC = GetComponent<WeaponController>();
        collider = gameObject.GetComponent<Collider>();
        wC.SetupRaycastWeapon(range, transform, collider, wC.TargetLayer);
    }

    protected override void Update()
    {
        if (!isPreview)
        {
            if (isCountingDown)
            {
                countDown -= Time.deltaTime;
            }
            if (countDown <= 0f)
            {
                wC.Fire();
                Delete(); //Hasta la vista, baby
            }

            if (!isCountingDown)
            {
                enemyList = TargetsInRange.FindTargets(360, activationDistance, transform, collider, wC.TargetLayer);
            }
            else
            {
                enemyList = TargetsInRange.FindTargets(360, range, transform, collider, wC.TargetLayer);
            }

            if (enemyList != null)
            {
                target = TargetsInRange.GetClosestEnemy(enemyList, transform);
                if (target != null)
                {
                    if (!isCountingDown)
                    {
                        isCountingDown = true; //Arming mine
                    }
                }
            }
        }
    }
}
