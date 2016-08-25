using UnityEngine;
using System.Collections;

public class Teleport : DefensiveSpell {

    private PlayerMovement2D movement;

    public float range = 3f;
    public GameObject teleportEffect;

    protected override void ActivateSpell() {

        float effectRange = range;
        Vector2 startLoc = transform.position;

        Collider2D hit = Physics2D.OverlapCircle(transform.position + transform.right * range * movement.GetRightValue, 0.1f, 1 << LayerMask.NameToLayer("Ground"));


        if (hit == null) {
            transform.position += transform.right * range * movement.GetRightValue;
        } else {
            RaycastHit2D lineHit = Physics2D.Linecast(transform.position, transform.position + transform.right * range * 1.2f * movement.GetRightValue, 1 << LayerMask.NameToLayer("Ground"));
            transform.position = lineHit.point;
            print(lineHit.point);
            effectRange = lineHit.distance;
        }

        if (teleportEffect != null) {
            GameObject effect = Instantiate(teleportEffect, startLoc, Quaternion.identity) as GameObject;
            effect.transform.localScale = new Vector3(effectRange * movement.GetRightValue, effect.transform.localScale.y, effect.transform.localScale.z);
            Destroy(effect, 3f);
        }

    }

    protected override void Init() {
        manaCost = GetComponent<Mana>().maxMana * 0.20f;
        movement = GetComponent<PlayerMovement2D>();
        spellName = "Teleport";
        teleportEffect = Resources.Load<GameObject>("Teleport");
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position + transform.right * range * movement.GetRightValue, 0.1f);
    }
}
