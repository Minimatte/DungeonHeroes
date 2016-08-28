using UnityEngine;
using System.Collections;

public class BeholderShield : Health {
    public int shieldId = 0;

    public override void Kill() {
        GetComponentInParent<BeholderHealth>().shields -= 1;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInParent<BeholderHealth>().TakeDamage(0);
    }

    public override void TakeDamage(float damageToTake) {
        if (currentHealth == 0)
            return;

        base.TakeDamage(damageToTake);
        if (GetComponentInParent<Animator>())
            GetComponentInParent<Animator>().SetTrigger("TakeDamage" + shieldId);
    }
}
