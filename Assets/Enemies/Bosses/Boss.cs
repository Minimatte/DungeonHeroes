using UnityEngine;
using System.Collections;

public class Boss : EnemyFlying {

    private bool canAttack { get { return currentCooldown == 0; } }
    protected float currentCooldown = 0;

    protected bool flips = true;

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
        if (flips)
            Flip();

        Movement();
    }

    protected virtual void Movement() { }

}
