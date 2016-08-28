using UnityEngine;
using System.Collections;

public class BeholderHealth : Health {


    public bool hasShield { get { return shields > 0; } }
    public int shields = 4;

    public override void TakeDamage(float damageToTake) {
        if (!hasShield) {
            if (transform.childCount > 0)
                foreach (Transform child in transform)
                    Destroy(child.gameObject);

            base.TakeDamage(damageToTake);
        }
    }

    void Update() {
        if (!hasShield)
            hasDamageFrames = true;
        else
            hasDamageFrames = false;
    }
}
