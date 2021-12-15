using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region Member Variables
    #region Public Variables
    public GameObject weaponMesh;
    public Material blueGhostMaterial;
    public Material redGhostMaterial;
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
    protected Material[] defaultMaterialArray;
    protected bool ghostIsRed = false;
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

        MeshRenderer[] mRList = gameObject.GetComponentsInChildren<MeshRenderer>();
        defaultMaterialArray = new Material[mRList.Length];
        for (int i = 0; i < defaultMaterialArray.Length; i++)
        {
            defaultMaterialArray[i] = mRList[i].material;
        }
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
        Vector3 scale = transform.localScale;
        scale.x *= 1.01f;
        scale.y *= 1.01f;
        scale.z *= 1.01f;
        transform.localScale = scale;
        MeshRenderer[] mRList = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mr in mRList)
        {
            mr.material = blueGhostMaterial;
        }
        Destroy(gameObject.GetComponent<Collider>());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public void SetGhostOnOff(bool aSwitch)
    {
        MeshRenderer[] mRList = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (aSwitch)
        {
            if (!ghostIsRed)
            {
                foreach (MeshRenderer mr in mRList)
                {
                    mr.material = blueGhostMaterial;
                }
            }
            else
            {
                foreach (MeshRenderer mr in mRList)
                {
                    mr.material = redGhostMaterial;
                }
            }
        }
        else
        {
            for (int i = 0; i < defaultMaterialArray.Length; i++)
            {
                mRList[i].material = defaultMaterialArray[i];
            }
        }
    }

    public void SetGhostColour(bool aSwitch) //True for blue, false for red
    {
        ghostIsRed = aSwitch;

        MeshRenderer[] mRList = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (!ghostIsRed)
        {
            foreach (MeshRenderer mr in mRList)
            {
                mr.material = blueGhostMaterial;
            }
        }
        else
        {
            foreach (MeshRenderer mr in mRList)
            {
                mr.material = redGhostMaterial;
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
