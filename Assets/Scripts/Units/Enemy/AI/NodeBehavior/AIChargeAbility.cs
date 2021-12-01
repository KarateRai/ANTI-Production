using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Charge Ability")]
public class AIChargeAbility : AIAbility
{
    int chargeSpeed;

    public override IEnumerator Activate(EnemyController controller, Transform target)
    {
        controller.transform.LookAt(target);
        controller.Channeling = true;
        yield return new WaitForSeconds(castTime);
        controller.Channeling = false;
        Vector3 direction = (controller.transform.position - target.position).normalized;
        Rigidbody rb = controller.GetComponent<Rigidbody>();
        direction.y = rb.velocity.y;
        rb.velocity = direction * chargeSpeed;

    }
}
