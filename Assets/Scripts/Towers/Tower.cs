using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region Member Variables
    #region Public Variables
    public GameObject weaponMesh;
    public float range = 10;
    public bool shouldBeDeleted = false;
    public GameObject parentCell;
    public List<ParticleSystem> particleSystemList;
    public int health = 100;
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
    }

    public void TakeDamage(int aAmount)
    {
        health -= aAmount;
        if (health <= 0.0f)
        {
            Delete();
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
