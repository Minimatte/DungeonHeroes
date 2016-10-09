using UnityEngine;
using System.Collections;

public class GhostAI : EnemyFlying {

    public GameObject ghostProjectile;

    private bool canAttack { get { return currentCooldown == 0; } }
    private float currentCooldown = 0;
    private Vector2 playerOffset = Vector2.zero;
    private float playerOffsetMultiplier = 4;


    void Update() {

        if (!canAttack)
            currentCooldown = Mathf.Clamp(currentCooldown - Time.deltaTime, 0, attackProperties.cooldown);
        if (target != null && Vector2.Distance(transform.position, target.position) > aiProperties.aggroRange * 1.5f) {
            target = null;
            playerOffset = Vector2.zero;
        }

        if (target != null) {
            if (playerOffset == Vector2.zero ) {
                playerOffsetMultiplier = Random.Range(playerOffsetMultiplier / 2, playerOffsetMultiplier);
               // playerOffset = new Vector2(Mathf.Abs(playerOffset.x), Mathf.Abs(playerOffset.y));
                playerOffset = Random.insideUnitCircle * playerOffsetMultiplier;
            }

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
        //if (attackProperties.)
        
            GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(transform.position, (Vector2)target.transform.position + (Vector2.right * attackProperties.range * rightOrLeft) + Vector2.up * (Mathf.Sin(Time.frameCount / 50f) / speed), speed * Time.deltaTime));
        //    GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(transform.position, ((Vector2)target.position + playerOffset) + Vector2.up * (Mathf.Sin(Time.frameCount / 50f) / speed), speed * Time.deltaTime));
    }

    protected override void Attack() {

        var direction = (target.position - transform.position);
        var dir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject go = (GameObject)Instantiate(ghostProjectile, transform.position, Quaternion.Euler(new Vector3(0, 0, dir))) as GameObject;
        go.GetComponent<Projectile>().damage = attackProperties.damage;
        currentCooldown = attackProperties.cooldown;
    }


}
