using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem impactParticles;

    //-----------------Send variables-------------------//
    public Ability.AbilityType typeOfAttack;
    private int _amount = 0;
    public int Amount { get { return _amount; } set { _amount = value; } }

    //Remove later
    //Set color from weapon 
    //public Gradient psColor;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        //Should be positioned in weapon
        ParticleSystem.MainModule psMain = particleLauncher.main;
        //psMain.startColor = psColor;
    }
    private void OnParticleCollision(GameObject other)
    {
        UnitController controller = other.GetComponent<UnitController>();
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        switch (typeOfAttack)
        {
            case Ability.AbilityType.ATTACK:
                controller.TakeDamage(_amount);
                break;
            case Ability.AbilityType.BUFF:
                //Not yet implemented
                break;
            case Ability.AbilityType.DEBUFF:
                controller.AffectSpeed(_amount);
                break;
            case Ability.AbilityType.HEAL:
                controller.GainHealth(_amount);
                break;
        }

        if (impactParticles)
        {
            for (int i = 0; i < collisionEvents.Count; i++)
            {
                EmitAtLocation(collisionEvents[i]);
            }
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        impactParticles.transform.position = particleCollisionEvent.intersection;
        impactParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        impactParticles.Emit(10);
    }

}
