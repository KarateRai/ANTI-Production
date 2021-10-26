using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    ///-------------------------AI variables-------------------------------///

    [HideInInspector] public EnemyController controller;
    [HideInInspector] public Weapon weapon;
    protected Node topNode;
    protected NavMeshAgent agent;

    ///--------------------Targetting variables----------------------------///
    [HideInInspector]
    public List<GameObject> targetsInRange = new List<GameObject>();
    [HideInInspector]
    public GameObject closestTarget;

    protected bool IsInit = false;

    public void Awake()
    {
        this.controller = GetComponent<EnemyController>();
        agent = controller.GetComponent<NavMeshAgent>();
        this.tag = "AI";
    }
    private void Update()
    {
        Tick();
    }
    public abstract void Tick();

    public abstract void InitializeAI(EnemyController controller);
}
