using UnityEngine;
using System.Collections;

public class AngelShieldHealth : Health {

    public override void TakeDamage(float damageToTake) {
        if (damageToTake > 0)
            damageToTake = 1;
        base.TakeDamage(damageToTake);
    }
}
