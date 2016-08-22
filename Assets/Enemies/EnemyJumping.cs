using UnityEngine;
using System.Collections;

public class EnemyJumping : Enemy {

    private Rigidbody2D rbody;
    public float patrolRange = 1;
    public float upForce, forwardForce;
    public float jumpInterval = 2;

    void Awake() {
        rbody = GetComponent<Rigidbody2D>();

        RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.down * int.MaxValue, 1 << LayerMask.NameToLayer("Ground"));

        if (hit.collider != null)
            startLocation = hit.point;
        else
            startLocation = transform.position;

        patrolLocation = startLocation;

        InvokeRepeating("JumpTowardsPoint", jumpInterval, jumpInterval);
    }

    void Update() {
        if (target == null)
            ChangePatrolTarget();
        CheckForTarget();

    }

    protected void ChangePatrolTarget() {
        int rightOrLeft = -1;

        if (Vector2.Distance(transform.position, patrolLocation) < 0.5f) {


            rightOrLeft = Random.Range(0, 2) == 0 ? 1 : -1;
            patrolLocation = (Vector2)transform.position + Vector2.right * rightOrLeft * patrolRange;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, patrolLocation, 1 << LayerMask.NameToLayer("Ground"));
            if (hit.collider != null)
                patrolLocation = hit.point;
        }
    }

    protected virtual void JumpTowardsPoint() {

        if (!GetComponent<Collider2D>().IsTouchingLayers(1 << LayerMask.NameToLayer("Ground")))
            return;

        if (target == null) {
            Jump(patrolLocation);
        } else
            Jump(target.transform.position);

        Flip();
    }

    private void Jump(Vector3 targetLocation) {
        Vector2 direction = (targetLocation - transform.position).normalized;
        rbody.AddForce(Vector2.up * upForce + (direction * forwardForce), ForceMode2D.Impulse);
    }

    public void OnDrawGizmos() {
        if (Application.isPlaying) {
            Gizmos.DrawCube(patrolLocation, Vector3.one);
            Gizmos.DrawWireSphere(transform.position, aiProperties.aggroRange);
        }
    }
}
