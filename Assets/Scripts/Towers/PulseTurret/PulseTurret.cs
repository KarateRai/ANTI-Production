using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTurret : MonoBehaviour
{
    public GameObject myHousing;
    public Animator myAnimator;
    public GameObject target;
    float countDown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f)
        {
            countDown = 2.0f;
            myAnimator.Play("Scene");
        }
        myHousing.GetComponent<PulseHousing>().Target(target.transform.position);
    }
}