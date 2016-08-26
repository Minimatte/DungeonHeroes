using UnityEngine;
using System.Collections;

public class FireballProjectile : Projectile {

    public GameObject hitEffect;

    public LayerMask hitLayers;

    void OnCollisionEnter2D(Collision2D other) {
        if (hitLayers == (hitLayers | (1 << other.gameObject.layer))) {

            GetComponent<Rigidbody2D>().isKinematic = true;
            transform.SetParent(other.transform, true);
            if (other.gameObject.GetComponent<Health>()) {
                other.gameObject.GetComponent<Health>().takeDamage(damage);

            }
            GetComponent<Collider2D>().enabled = false;
            if (destroyOnImpact)
                Destroy(gameObject);
        }

    }

    public void OnDestroy() {
        if (hitEffect != null)
            Destroy(Instantiate(hitEffect, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360))), 3);
    }
}
