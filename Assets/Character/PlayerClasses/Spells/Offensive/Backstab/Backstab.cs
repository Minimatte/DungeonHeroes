using UnityEngine;
using System.Collections;

public class Backstab : OffensiveSpell {

    private GameObject spellEffect;
    private float backstabMultip = 2.2f;
    private PlayerMovement2D movement;
    private float range = 1;
    private LayerMask hitmask;

    private AudioClip attackSound;

    protected override void Init() {
        attackSound = Resources.Load<AudioClip>("BackstabAttackSound");
        GetComponent<SpellHandler>().canStrafe = true;
        spellName = "Backstab";
        manaCost = 2;
        power = 4;
        movement = GetComponent<PlayerMovement2D>();
        hitmask = (1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("FlyingEnemy"));
        spellEffect = Resources.Load<GameObject>("Backstab");
    }

    protected override void ActivateSpell() {
        if (attackSound != null)
            AudioSource.PlayClipAtPoint(attackSound, transform.position);

        Vector2 attackLocation = (Vector2)transform.position + Vector2.right * movement.GetRightValue * range;
        Collider2D hit = Physics2D.OverlapCircle(attackLocation, range, hitmask);
        GameObject go = (GameObject)Instantiate(spellEffect, attackLocation, Quaternion.identity);
        go.transform.localScale = new Vector3(go.transform.localScale.x * -movement.GetRightValue, go.transform.localScale.y, go.transform.localScale.z);
        Destroy(go, 2); // destroy the effect in 2 seconds.
        if (hit != null) {
            float multip = 1;
            if (hit.gameObject.GetComponent<Enemy>() != null) { // if the health script is on the object we hit

                multip = Vector2.Dot(movement.GetRightVector, hit.gameObject.GetComponent<Enemy>().GetRightVector) > 0 ? 1 : backstabMultip;
                if (multip == backstabMultip)
                    go.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            } else { // if the health script is in the objects parent.
                multip = Vector2.Dot(movement.GetRightVector, hit.gameObject.GetComponentInParent<Enemy>().GetRightVector) > 0 ? 1 : backstabMultip;
                if (multip == backstabMultip)
                    go.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
            hit.GetComponent<Health>().TakeDamage((PlayerHeroes.GetPlayerPower + power) * multip);
        }
    }
}
