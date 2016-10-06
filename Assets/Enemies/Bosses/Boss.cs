using UnityEngine;
using System.Collections;

public class Boss : EnemyFlying {
    public bool active = false;
    private bool canAttack { get { return currentCooldown == 0; } }
    protected float currentCooldown = 0;

    protected bool flips = true;

    void Start() {
        BossRoom.currentBoss = gameObject;
    }

    void Update() {
        if (!active)
            return;

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
