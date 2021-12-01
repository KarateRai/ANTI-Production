using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTurret : Tower
{
    // Update is called once per frame
    protected override void Update()
    {

        countDown -= Time.deltaTime;
        if (countDown <= 0f)
        {
            countDown = 2.0f;
        }
        if (!isPreview)
        {
            countDown -= Time.deltaTime;
            enemyList = TargetsInRange.FindTargets(360, range, transform, collider, wC.TargetLayer);
            target = TargetsInRange.GetClosestEnemy(enemyList, transform);

            if (target != null)
            {
                if (countDown <= 0f)
                {
                    countDown = 2.0f;
                    wC.Fire();
                    foreach (ParticleSystem ps in particleSystemList)
                    {
                        ps.Play();
                    }
                }

                //Enemy Tracking
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, target.transform.position - transform.position);
                targetRotation *= Quaternion.Euler(0, 0, -180);
                weaponMesh.transform.rotation = targetRotation;
            }
        }
    }
}