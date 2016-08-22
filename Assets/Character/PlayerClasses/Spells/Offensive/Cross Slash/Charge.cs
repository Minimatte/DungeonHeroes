using UnityEngine;
using System.Collections;

public class Charge : OffensiveSpell {

    private PlayerMovement2D movement;

    private float speed = 10;

    public float range = 7;

    public float degrees = 30;

    protected override void Init() {
        movement = GetComponent<PlayerMovement2D>();
        manaCost = 5;
        spellName = "Charge";
    }

    public override void UseSpell() {
        if (movement.canMove)
            base.UseSpell();
    }

    protected override void ActivateSpell() {

        Vector3 targetLocation = transform.position + (transform.right * movement.GetRightValue * range);

        StartCoroutine(MovePlayer(targetLocation));
    }

    private IEnumerator MovePlayer(Vector3 target) {
        movement.canMove = false;
        float gravity = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, target, 1 << 8);

        if (hit)
            target = hit.point;

        while (transform.position != target) {
            transform.position = Vector3.LerpUnclamped(transform.position, target, speed * Time.deltaTime);
            yield return 0;

            if (Vector3.Distance(transform.position, target) < 0.5f)
                break;

        }
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        yield return new WaitForSeconds(0.25f);
        movement.canMove = true;

    }
}
