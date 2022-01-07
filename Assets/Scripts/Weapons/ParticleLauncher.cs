using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem impactParticles;

    //-----------------Send variables-------------------//
    public Ability.AbilityType typeOfAttack;
    private Weapon _weapon;
    public Weapon Weapon { get { return _weapon; } set { _weapon = value; } }

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        ParticleSystem.MainModule psMain = particleLauncher.main;
        if (impactParticles != null)
        {
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity);
            impactParticles.transform.parent = transform;
        }
        
        
    }
    private void OnParticleCollision(GameObject other)
    {
        UnitController controller = other.GetComponent<UnitController>();
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        switch (typeOfAttack)
        {
            case Ability.AbilityType.ATTACK:
                controller.TakeDamage(Weapon.Damage);
                break;
            case Ability.AbilityType.BUFF:
                //Not yet implemented
                break;
            case Ability.AbilityType.DEBUFF:
                controller.AffectSpeed(Weapon.Damage);
                break;
            case Ability.AbilityType.HEAL:
                controller.GainHealth(Weapon.Damage);
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
