using UnityEngine;
using System.Collections;

public class FlameTrap : Trap {

    public GameObject[] flames;

    public new void OnTriggerEnter2D(Collider2D collision) {
        if (currentCooldown > 0)
            return;

        if (anim != null && hitmask == (hitmask | (1 << collision.gameObject.layer))) {
            foreach (GameObject go in flames) {
                go.GetComponent<Animator>().SetTrigger("Trigger");
            }
            currentCooldown = cooldown;
            StartCoroutine(Flames());
        }
    }

    public override void DealDamage() {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + Vector3.up * 0.16f, Vector2.one, 90, hitmask);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].GetComponent<Health>()) {
                hits[i].GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator Flames() {
        PlayTrapSound();
        for( int i = 0; i < 7; i++) {
            DealDamage();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
