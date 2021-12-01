using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region Member Variables
    #region Public Variables
    public GameObject weaponMesh;
    public Animator animator;
    public float range = 10;
    public bool shouldBeDeleted = false;
    public GameObject parentCell;
    public List<ParticleSystem> particleSystemList;
    #endregion

    #region Private Variables
    public GameObject target;
    protected float countDown = 2.0f;
    protected Collider collider;
    protected List<GameObject> enemyList;
    protected WeaponController wC;
    protected bool isPreview = false;
    #endregion
    #endregion

    // Start is called before the first frame update
    protected void Start()
    {
        enemyList = new List<GameObject>();
        wC = GetComponent<WeaponController>();
        collider = gameObject.GetComponent<Collider>();
    }

    public void SetParentCell(GameObject aParentCell)
    {
        parentCell = aParentCell;
    }

    public void SetPreview()
    {
        isPreview = true;
    }

    // Update is called once per frame
    protected virtual void Update()
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
                        foreach (ParticleSystem ps in particleSystemList)
                        {
                            ps.Play();
                        }
                    }

                    //Enemy Tracking
                    Quaternion targetRotation = Quaternion.LookRotation((target.transform.position - (transform.position + weaponMesh.transform.position) / 2), Vector3.up);
                    //Find angle of pitch (up/down rotation) with trig equation sin(theta) = Opposite / Hypothenuse
                    float a = (target.transform.position - weaponMesh.transform.position).magnitude; //Base
                    float o = target.transform.position.y - (weaponMesh.transform.position.y); // Height
                    float h = Mathf.Sqrt((o * o) + (a * a)); //a2 + b2 = c2
                    float pitch = Mathf.Asin(o / h) * Mathf.Rad2Deg;
                    targetRotation *= Quaternion.Euler(pitch, 90, 0);
                    weaponMesh.transform.rotation = targetRotation;
                }
            }
        }
    }

    public void Delete()
    {
        shouldBeDeleted = true;
    }

    public void SetRange(float aRange)
    {
        range = aRange;
    }
}
