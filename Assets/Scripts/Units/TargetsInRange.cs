using System.Collections.Generic;
using UnityEngine;

public static class TargetsInRange
{
    public static List<GameObject> FindTargets(float angle, float range, Transform transform, Collider ownCollider, LayerMask targetLayer,
        LayerMask ignoreLayer)
    {
        List<GameObject> targetsInRange = new List<GameObject>();
        if (ownCollider == null)
        {
            return null;
        }
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, range, targetLayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            GameObject target = targetsInViewRadius[i].gameObject;
            Transform targetTansform = target.GetComponent<Transform>();
            Vector3 directionToTarget = (targetTansform.position - transform.position).normalized;


            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTansform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ignoreLayer)
                    && targetsInViewRadius[i] != ownCollider)
                {
                    if (target.layer != ownCollider.gameObject.layer)
                    {
                        targetsInRange.Add(target);
                    }
                }
            }
            Debug.DrawLine(transform.position, targetTansform.position, Color.white);
        }
        return targetsInRange.Count == 0 ? null : targetsInRange;
    }

    public static Transform GetClosestEnemy(Transform[] enemies, Transform me)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    public static GameObject GetClosestEnemy(List<GameObject> enemies, Transform me)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
