using UnityEngine;
using System.Collections;

public class SlimeHealth : Health {

    private Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float damageToTake) {
        base.TakeDamage(damageToTake);

        if (anim != null)
            anim.SetTrigger("TakeDamage");
    }

}
