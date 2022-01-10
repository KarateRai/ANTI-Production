using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    ///-------------------------AI variables-------------------------------///

    [HideInInspector] public EnemyController controller;
    protected Node topNode;
    protected NavMeshAgent agent;
    protected bool isStopped = false;

    ///--------------------Targetting variables----------------------------///
    [HideInInspector]
    public List<GameObject> targetsInRange = new List<GameObject>();
    [HideInInspector]
    public GameObject closestTarget;
    public float range;

    protected bool IsInit = false;

    [SerializeField] public AIAbility[] abilities;

    public void Awake()
    {
        this.controller = GetComponent<EnemyController>();
        agent = controller.GetComponent<NavMeshAgent>();
        this.tag = "AI";
        isStopped = false;
    }

    private void Update()
    {
        if (isStopped)
            return;

        //If we are channeling, stop.
        if (controller.Channeling == true)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        //Update speed
        if (agent.speed != controller.Stats.Speed)
        {
            agent.speed = controller.Stats.Speed;
        }
       
        Tick();
    }
    public void Tick()
    {
        if (!IsInit)
        {
            InitializeAI(controller);
            return;
        }

        topNode.Evaluate();
        if (topNode.State == Node.NodeState.FAILURE)
        {
            Debug.LogError("TopNode returned FAILURE!");
            agent.isStopped = true;
        }
    }

    public abstract void InitializeAI(EnemyController controller);
    protected virtual void SetupParticleWeapon()
    {
        controller.weaponController.SetShootingPos();
        controller.weaponController.SetupParticleWeapon();
    }
    protected virtual void SetupRaycastWeapon()
    {
        controller.weaponController.SetupRaycastWeapon(range, transform, GetComponent<Collider>(), controller.weaponController.TargetLayer);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
        isStopped = true;
    }
}
