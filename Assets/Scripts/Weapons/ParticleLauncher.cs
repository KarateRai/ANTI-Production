using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem impactParticles;

    //Set color from weapon
    public Gradient psColor;

    List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        ParticleSystem.MainModule psMain = particleLauncher.main;
        psMain.startColor = psColor;
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
        
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        impactParticles.transform.position = particleCollisionEvent.intersection;
        impactParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        impactParticles.Emit(10);
    }

}
