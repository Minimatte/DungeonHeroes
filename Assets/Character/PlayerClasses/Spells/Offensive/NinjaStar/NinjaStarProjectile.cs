using UnityEngine;
using System.Collections;

public class NinjaStarProjectile : Projectile {
    public GameObject sprite;
    public LayerMask hitLayers;
    private bool hasCollided = false;
    void Update() {
        Vector3 rot = sprite.transform.eulerAngles;
        if (!hasCollided)
            sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.Euler(rot.x, rot.y, rot.z - 25), 60 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (hitLayers == (hitLayers | (1 << other.gameObject.layer))) {
            hasCollided = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
            transform.SetParent(other.transform, true);
            if (other.gameObject.GetComponent<Health>())
                other.gameObject.GetComponent<Health>().TakeDamage(damage);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
