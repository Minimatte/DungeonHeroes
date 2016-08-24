using UnityEngine;
using System.Collections;

public class BeholderShield : Health {
    public int shieldId = 0;

    public override void Kill() {
        GetComponentInParent<BeholderHealth>().shields -= 1;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInParent<BeholderHealth>().takeDamage(0);
    }

    public override void takeDamage(float damageToTake) {
        if (currentHealth == 0)
            return;

        base.takeDamage(damageToTake);
        if (GetComponentInParent<Animator>())
            GetComponentInParent<Animator>().SetTrigger("TakeDamage" + shieldId);
    }
}
