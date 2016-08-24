using UnityEngine;
using System.Collections;

public class SlimeAI : EnemyJumping {

    public GameObject slimeWalkEffect;

    protected override void Attack() {
        target.GetComponent<Health>().takeDamage(attackProperties.damage);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Attack();
        }

        if (slimeWalkEffect != null) {
            Destroy(Instantiate(slimeWalkEffect, collision.contacts[0].point + new Vector2(Mathf.Abs(transform.localScale.x / 2), 0.08f), Quaternion.identity), 3);
        }
    }

}
