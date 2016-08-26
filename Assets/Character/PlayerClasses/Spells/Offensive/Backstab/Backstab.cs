using UnityEngine;
using System.Collections;

public class Backstab : OffensiveSpell {

    private GameObject spellEffect;
    private float backstabMultip = 2.2f;
    private PlayerMovement2D movement;
    private float range = 1;
    private LayerMask hitmask;

    protected override void Init() {
        GetComponent<SpellHandler>().canStrafe = false;
        spellName = "Backstab";
        manaCost = 2;
        power = 4;
        movement = GetComponent<PlayerMovement2D>();
        hitmask = (1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("FlyingEnemy"));
        spellEffect = Resources.Load<GameObject>("Backstab");
    }

    protected override void ActivateSpell() {
        Vector2 attackLoc = (Vector2)transform.position + Vector2.right * movement.GetRightValue * range;
        Collider2D hit = Physics2D.OverlapCircle(attackLoc, range, hitmask);
        GameObject go = (GameObject)Instantiate(spellEffect, attackLoc, Quaternion.identity);
        go.transform.localScale = new Vector3(go.transform.localScale.x * -movement.GetRightValue, go.transform.localScale.y, go.transform.localScale.z);

        if (hit != null) {

            float multip = Vector2.Dot(movement.GetRightVector, hit.gameObject.GetComponent<Enemy>().GetRightVector) > 0 ? 1 : backstabMultip;
            if (multip == backstabMultip)
                go.GetComponentInChildren<SpriteRenderer>().color = Color.red;

            hit.GetComponent<Health>().takeDamage(power * multip);
        }
    }
}
