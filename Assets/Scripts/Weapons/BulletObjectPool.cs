using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool SharedInstance;
    public List<GameObject> pooledBullets;
    private List<Rigidbody> pooledBulletRigidbodies;
    public GameObject objectToPool;
    public int amountToPool;
    public float bulletSpeed = 3;
    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();
        pooledBulletRigidbodies = new List<Rigidbody>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.transform.parent = this.gameObject.transform;
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
            pooledBulletRigidbodies.Add(tmp.GetComponent<Rigidbody>());
        }
    }

    void Update()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (pooledBullets[i].activeSelf)
            {
                pooledBulletRigidbodies[i].velocity = transform.forward * bulletSpeed;
            }
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
