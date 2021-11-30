using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        controller.GetComponent<Rigidbody>().velocity = chargeSpeed * direction;

    }
}
