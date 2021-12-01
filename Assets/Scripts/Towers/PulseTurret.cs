using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTurret : Tower
{
    public GameObject weaponMeshSecond;
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
                    Quaternion targetRotation1 = Quaternion.LookRotation((target.transform.position - (transform.position + weaponMesh.transform.position)).normalized, Vector3.up);

                    targetRotation1 *= Quaternion.Euler(0, 0, 0);
                    Debug.Log(targetRotation1.eulerAngles);
                    weaponMesh.transform.rotation = targetRotation1;
                    
                    Quaternion targetRotation2 = Quaternion.LookRotation((target.transform.position - (transform.position + weaponMesh.transform.position)).normalized, Vector3.up);

                    //Find angle of pitch (up/down rotation) with trig equation sin(theta) = Opposite / Hypothenuse
                    float a = (target.transform.position - weaponMesh.transform.position).magnitude; //Base
                    float o = target.transform.position.y - (weaponMesh.transform.position.y); // Height
                    float h = Mathf.Sqrt((o * o) + (a * a)); //a2 + b2 = c2
                    float pitch = Mathf.Asin(o / h) * Mathf.Rad2Deg;
                    targetRotation2 *= Quaternion.Euler(pitch, 0, 0);
                    weaponMeshSecond.transform.rotation = targetRotation2;
                }
            }
        }
    }
}