using UnityEngine;
using System.Collections;

public class IcicleTrap : Trap {
    private bool hasActivated = false;
    void Start() {
        RaycastHit2D hit = Physics2D.Linecast((Vector2)transform.position + Vector2.up * 0.16f, (Vector2)transform.position + Vector2.up * 10, 1 << LayerMask.NameToLayer("Ground"));

        if(hit.collider == null || hit.collider.CompareTag("Object")) {
            Destroy(gameObject);
        } else {
            transform.position = hit.point;
        }
    }

    public new void OnTriggerEnter2D(Collider2D collision) {
        if (currentCooldown > 0)
            return;

        if (anim != null && hitmask == (hitmask | (1 << collision.gameObject.layer))) {

            GetComponent<Animator>().SetTrigger("Trigger");
            hasActivated = true;
        }
    }

}
