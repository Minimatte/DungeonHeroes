using UnityEngine;
using System.Collections;

public class RayThrustProjectile : Projectile {

    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public Transform owner;

    private bool goBack = false;

    void Update() {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;



        if (!canDamage)
            currentDamageCooldown = Mathf.Clamp(currentDamageCooldown - Time.deltaTime, 0, damageCooldown);

        if (!goBack) {
            transform.position = Vector3.MoveTowards(transform.position, target, forwardSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target) < 0.1f)
                goBack = true;
        } else {
            transform.position = Vector3.MoveTowards(transform.position, owner.position, forwardSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, owner.position) < 0.1f) {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ToggleCollision(Collision2D other) {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider, true);
        while (!canDamage) {
            yield return new WaitForSeconds(0.05f);

        }
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider, false);

    }

    void OnCollisionEnter2D(Collision2D other) {

        if (hitMask == (hitMask | (1 << other.collider.gameObject.layer)))
            if (other.gameObject.GetComponent<Health>()) {
                DealDamage(other.gameObject.GetComponent<Health>());
                StartCoroutine(ToggleCollision(other));
            }

        if (hitParticle != null)
            Destroy(Instantiate(hitParticle, transform.position, Quaternion.identity), 3);

        if (destroyOnImpact) {
            Destroy(gameObject);
        }
    }
}

