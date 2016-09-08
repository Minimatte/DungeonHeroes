using UnityEngine;
using System.Collections;

public class Slash : OffensiveSpell {

    private GameObject spellEffect;
    private PlayerMovement2D movement;
    private float range = 1;
    private LayerMask hitmask;

    private AudioClip soundEffect;

    protected override void Init() {
        soundEffect = Resources.Load<AudioClip>("SlashAttackSound");
        GetComponent<SpellHandler>().canStrafe = true;
        spellName = "Slash";
        manaCost = 5;
        power = 4;
        movement = GetComponent<PlayerMovement2D>();
        hitmask = (1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("FlyingEnemy"));
        spellEffect = Resources.Load<GameObject>("Slash");
    }

    protected override void ActivateSpell() {

        if (soundEffect != null)
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);

        Vector2 attackLoc = (Vector2)transform.position + Vector2.right * movement.GetRightValue * range;
        Collider2D hit = Physics2D.OverlapCircle(attackLoc, range, hitmask);
        GameObject go = (GameObject)Instantiate(spellEffect, attackLoc + Vector2.up * 0.16f, Quaternion.identity);
        go.transform.localScale = new Vector3(go.transform.localScale.x * movement.GetRightValue, go.transform.localScale.y, go.transform.localScale.z);
        go.transform.SetParent(transform);
        if (Random.Range(0, 100) > 50)
            go.GetComponentInChildren<SpriteRenderer>().flipY = true;
        else
            go.GetComponentInChildren<SpriteRenderer>().flipY = false;

        Destroy(go, 2);
        if (hit != null) {
            if (hit.GetComponent<Health>())
                hit.GetComponent<Health>().TakeDamage(power + PlayerHeroes.GetPlayerPower);
            else if (hit.GetComponentInParent<Health>())
                hit.GetComponentInParent<Health>().TakeDamage(power + PlayerHeroes.GetPlayerPower);

        }
    }
}
