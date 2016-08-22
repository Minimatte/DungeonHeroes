using UnityEngine;
using System.Collections;

public class EnemyFlying : Enemy {



    public float patrolRange = 1;
    public float speed;

    void Awake() {
        startLocation = transform.position;
        patrolLocation = startLocation + (Vector3)Random.insideUnitCircle * patrolRange;
    }


    void Update() {
        MoveTowardsPatrol();
        
    }



    protected virtual void MoveTowardsPatrol() {
        transform.position = Vector3.MoveTowards(transform.position, patrolLocation, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolLocation) < 0.1f) {
            patrolLocation = startLocation + (Vector3)Random.insideUnitCircle * patrolRange;
        }

    }

    void OnDrawGizmos() {
        if (Application.isPlaying) {
            Gizmos.DrawWireSphere(startLocation, patrolRange);
            Gizmos.DrawWireSphere(transform.position, aiProperties.aggroRange);
        } else
            Gizmos.DrawWireSphere(transform.position, patrolRange);
    }
}
