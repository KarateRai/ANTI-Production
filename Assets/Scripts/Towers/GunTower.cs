using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : Tower
{
    protected override void Update()
    {
        if (!isPreview)
        {
            countDown -= Time.deltaTime;
            enemyList = TargetsInRange.FindTargets(360, range, transform, collider, wC.TargetLayer);

            if (enemyList != null)
            {
                target = TargetsInRange.GetClosestEnemy(enemyList, transform);
                if (target != null)
                {
                    if (countDown <= 0f)
                    {
                        countDown = 2.0f;
                        wC.Fire();
                        if (particleSystemList != null)
                        {
                            foreach (ParticleSystem ps in particleSystemList)
                            {
                                ps.Play();
                            }
                        }
                    }

                    //Enemy Tracking
                    Quaternion targetRotation = Quaternion.LookRotation((target.transform.position - (transform.position + weaponMesh.transform.position) / 2), Vector3.up);
                    //Find angle of pitch (up/down rotation) with trig equation sin(theta) = Opposite / Hypothenuse
                    float a = (target.transform.position - weaponMesh.transform.position).magnitude; //Base
                    float o = target.transform.position.y - (weaponMesh.transform.position.y + 0.5f); // Height
                    float h = Mathf.Sqrt((o * o) + (a * a)); //a2 + b2 = c2
                    float pitch = Mathf.Asin(o / h) * Mathf.Rad2Deg;
                    //targetRotation *= Quaternion.Euler(pitch, 90, 0);
                    weaponMesh.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 90, -pitch);
                }
            }
        }
    }
}
