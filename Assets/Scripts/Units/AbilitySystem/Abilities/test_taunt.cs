using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_taunt : MonoBehaviour
{
    public float range;
    public float duration;
    public int damage;
    public GameObject tauntTarget;
    public static Collider[] colliders;
    public LayerMask layerMask;// set in editor, on the prefab
    private GameObject originalTarget;

    // Start is called before the first frame update
    void Start()
    {
        colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);
        foreach (Collider c in colliders)
        {
            if (c.tag == "AI")
            {
                originalTarget = c.GetComponent<EnemyController>().toObjPosition;
                c.GetComponent<EnemyController>().toObjPosition = tauntTarget;
            }
        }
    }

    public IEnumerator DelayedResetSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Collider c in colliders)
        {
            if (c.tag == "AI")
            {
                c.GetComponent<EnemyController>().toObjPosition = originalTarget;
            }
        }
        Destroy(gameObject);
    }
}
