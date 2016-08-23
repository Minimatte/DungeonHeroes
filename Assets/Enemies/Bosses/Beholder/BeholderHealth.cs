using UnityEngine;
using System.Collections;

public class BeholderHealth : Health {


    public bool hasShield { get { return shields > 0; } }
    public int shields = 4;

    public override void takeDamage(float damageToTake) {
        if (!hasShield)
            base.takeDamage(damageToTake);
    }

    void Update() {
        if (!hasShield)
            hasDamageFrames = true;
        else
            hasDamageFrames = false;
    }
}
