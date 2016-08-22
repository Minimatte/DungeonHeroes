using UnityEngine;
using System.Collections;

public class EnemyCharging : Enemy {

    private bool isCharging = false;

    public float speed = 1;
    public float patrolRange = 4;

    public float chargeCooldownMax = 5;
    private float chargeCooldown = 0;

    private bool canCharge { get { return chargeCooldown == 0; } }

    void Awake() {

        RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.down * int.MaxValue, 1 << LayerMask.NameToLayer("Ground"));

        if (hit.collider != null)
            startLocation = hit.point + Vector2.up;
        else
            startLocation = transform.position;

        patrolLocation = startLocation;

    }

    protected virtual void MoveTowardsPatrol() {
        transform.position = Vector3.MoveTowards(transform.position, patrolLocation, speed * Time.deltaTime);
        int rightOrLeft = -1;

        if (Vector3.Distance(transform.position, patrolLocation) < 0.1f) {
            rightOrLeft = Random.Range(0, 2) == 0 ? 1 : -1;
            patrolLocation = (Vector2)transform.position + Vector2.right * rightOrLeft * patrolRange;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, patrolLocation, 1 << LayerMask.NameToLayer("Ground"));
            if (hit.collider != null)
                patrolLocation = hit.point;
        }

    }

    private void MoveTowardsTarget() {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);


    }

    void Update() {

        chargeCooldown = Mathf.Clamp(chargeCooldown - Time.deltaTime, 0, chargeCooldownMax);

        if (isCharging)
            return;
        else
        if (canCharge && target != null) {
            StartCoroutine(Charge());
            print("Charging");
        }

        if (target != null)
            MoveTowardsTarget();
        else
            MoveTowardsPatrol();

        CheckForTarget();
        Flip();
    }

    public void OnDrawGizmos() {
        if (Application.isPlaying)
            Gizmos.DrawWireCube(patrolLocation, Vector3.one);
    }

    protected IEnumerator Charge() {
        isCharging = true;
        chargeCooldown = chargeCooldownMax;


        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + (-Vector3.right * transform.localScale.x) * 5, 1 << LayerMask.NameToLayer("Ground"));
        Vector3 endPos;
        if (hit.collider != null)
            endPos = hit.point;
        else
            endPos = transform.position + (-Vector3.right * transform.localScale.x) * 5;
        patrolLocation = endPos;
        yield return new WaitForSeconds(1);
        while (Vector2.Distance(transform.position, endPos) > 1.5f) {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime * 10);
            yield return 0;
        }
        yield return new WaitForSeconds(1);
        isCharging = false;
        yield return null;
    }
}
