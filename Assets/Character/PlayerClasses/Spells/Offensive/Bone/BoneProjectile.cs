using UnityEngine;
using System.Collections;

public class BoneProjectile : Projectile {

    public GameObject hitEffect;

    void Awake() {
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();

        rBody.AddForce(Vector2.up * 500);
        rBody.AddTorque(Random.Range(5, 45));
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (hitMask == (hitMask | (1 << other.gameObject.layer))) {


            if (other.gameObject.GetComponent<Health>()) {
                other.gameObject.GetComponent<Health>().TakeDamage(damage);

            }
           
            if (destroyOnImpact)
                Destroy(gameObject);
        }

    }

    public void OnDestroy() {
        if (hitEffect != null)
            Destroy(Instantiate(hitEffect, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360))), 3);
    }
}
