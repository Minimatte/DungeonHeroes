using UnityEngine;
using System.Collections;

public class GhostAI : EnemyFlying {

    public GameObject ghostProjectile;

    private bool canAttack { get { return currentCooldown == 0; } }
    private float currentCooldown = 0;

    void Update() {

        if (!canAttack)
            currentCooldown = Mathf.Clamp(currentCooldown - Time.deltaTime, 0, attackProperties.cooldown);


        if (target != null) {
            MoveTowardsTarget();
            if (canAttack)
                Attack();
        } else {
            MoveTowardsPatrol();
            CheckForTarget();
        }
        Flip();
    }


    void MoveTowardsTarget() {

        var rightOrLeft = (target.position.x - transform.position.x) > 0 ? -1 : 1;
        GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(transform.position, target.transform.position + (Vector3.right * attackProperties.range * rightOrLeft) + Vector3.up * (Mathf.Sin(Time.frameCount / 50f) / speed), speed * Time.deltaTime));
      

    }

    protected override void Attack() {

        var direction = (target.position - transform.position);
        var dir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(ghostProjectile, transform.position, Quaternion.Euler(new Vector3(0, 0, dir)));
        currentCooldown = attackProperties.cooldown;
    }
}
