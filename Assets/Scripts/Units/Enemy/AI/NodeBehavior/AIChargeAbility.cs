using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Charge Ability")]
public class AIChargeAbility : AIAbility
{
    public int chargeSpeed;
    public float changeTime;
    public int damage;
    public float damageRange;
    [SerializeField] ParticleSystem particleEffect;

    public override IEnumerator Activate(EnemyController controller, Transform target)
    {
        controller.transform.LookAt(target);
        controller.Channeling = true;
        yield return new WaitForSeconds(castTime);
        controller.Channeling = false;
        Vector3 direction = (target.position - controller.transform.position).normalized;
        Rigidbody rb = controller.GetComponent<Rigidbody>();
        direction.y = rb.velocity.y;
        rb.velocity = direction * chargeSpeed;
        if (Vector3.Distance(target.position,  controller.transform.position) < damageRange)
        {
            particleEffect.Play();
            controller.TakeDamage(damage);
        }
        yield return new WaitForSeconds(changeTime);
        rb.velocity = Vector3.zero;
    }
}
