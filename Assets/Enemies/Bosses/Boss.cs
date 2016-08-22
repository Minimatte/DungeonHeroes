using UnityEngine;
using System.Collections;

public class Boss : EnemyFlying {

    private bool canAttack { get { return currentCooldown == 0; } }
    protected float currentCooldown = 0;



    void Update() {

        if (!canAttack)
            currentCooldown = Mathf.Clamp(currentCooldown - Time.deltaTime, 0, attackProperties.cooldown);


        if (target != null) {
            if (canAttack) {
                Attack();
                currentCooldown = attackProperties.cooldown;
            }
        } else {
            CheckForTarget();
        }
        Flip();
    }
}
