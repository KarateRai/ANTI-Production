using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    ///-------------------------AI variables-------------------------------///

    [HideInInspector] public EnemyController controller;
    protected Node topNode;
    protected NavMeshAgent agent;

    ///--------------------Targetting variables----------------------------///
    [HideInInspector]
    public List<GameObject> targetsInRange = new List<GameObject>();
    [HideInInspector]
    public GameObject closestTarget;
    public float range;

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
    public void Tick()
    {
        if (!IsInit)
        {
            InitializeAI(controller);
            return;
        }
        //if (agent.speed != controller.Stats.Speed)
        //{
        //    agent.speed = controller.Stats.Speed;
        //}

        topNode.Evaluate();
        if (topNode.State == Node.NodeState.FAILURE)
        {
            Debug.LogError("TopNode returned FAILURE!");
            agent.isStopped = true;
        }
    }

    public abstract void InitializeAI(EnemyController controller);
    protected virtual void SetupWeapon()
    {
        controller.weaponController.SetupRaycastWeapon(range, transform, GetComponent<Collider>(), controller.weaponController.TargetLayer);
    }
}
