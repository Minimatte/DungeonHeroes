using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    protected Vector3 startLocation;
    protected Vector3 patrolLocation;

    private bool facingRight = false;

    public HealthProperties healthProperties;
    public AttackProperties attackProperties;
    public AIProperties aiProperties;

    public Transform target;

    public float GetRightValue { get { return transform.localScale.x; } }
    public Vector3 GetRightVector { get { return new Vector3(transform.localScale.x, 0, 0); } }

    protected virtual void Attack() { }

    protected virtual bool CheckForTarget() {

        if (target != null) {
            if (Vector2.Distance(transform.position, target.transform.position) >= aiProperties.aggroRange)
                target = null;
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, aiProperties.aggroRange, 1 << LayerMask.NameToLayer("Player"));

        if (hit != null) {
            target = hit.transform;
            return true;
        } else
            return false;
    }

    protected void Flip() {
        bool isRight;
        if (target != null)
            isRight = (transform.position - target.transform.position).x < 0 ? true : false;
        else
            isRight = (transform.position - patrolLocation).x < 0 ? true : false;

        if (isRight != facingRight) {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
