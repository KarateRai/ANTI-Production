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
    public GameObject parentPlayer;
    public List<ParticleSystem> particleSystemList;
    public float countDown = 2.0f;
    #endregion

    #region Private Variables
    public GameObject target;
    protected Collider collider;
    protected List<GameObject> enemyList;
    protected WeaponController wC;
    protected bool isPreview = false;
    protected float lifeTimeCounter = 5f;
    #endregion
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyList = new List<GameObject>();
        wC = GetComponent<WeaponController>();
        wC.equippedWeapon = Object.Instantiate(wC.equippedWeapon);
        collider = gameObject.GetComponent<Collider>();
        wC.SetShootingPos();
    }

    public void SetParents(GameObject aParentCell, GameObject aParentPlayer)
    {
        parentCell = aParentCell;
        parentPlayer = aParentPlayer;
    }

    public void SetPreview()
    {
        isPreview = true;
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
        Destroy(gameObject.GetComponent<Collider>());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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
