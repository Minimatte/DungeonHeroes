using UnityEngine;
using System.Collections;

public class CrystalEnemyHealth : Health {

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float damageToTake) {
        base.TakeDamage(damageToTake);
        anim.SetTrigger("TakeDamage");
    }
}
