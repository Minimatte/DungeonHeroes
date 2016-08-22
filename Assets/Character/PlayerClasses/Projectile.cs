using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Projectile : MonoBehaviour {
    public float forwardSpeed;
    public bool useGravity;
    public bool destroyOnImpact;
    public float damage;
    public float lifetime = 1f;
    public bool useTrigger = false;


    public float damageCooldown;
    private float currentDamageCooldown = 0;
    private bool canDamage {
        get { return currentDamageCooldown == 0; }
    }

    public void setProjectileSettings(ProjectileSettings settings) // Instantiate an object with this class on, and then call this mehod with a settings object
    {
        forwardSpeed = settings.forwardSpeed;
        useGravity = settings.useGravity;
        destroyOnImpact = settings.destroyOnImpact;
        damage = settings.damage;
        lifetime = settings.lifetime;
    }

    void Start() {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right * forwardSpeed);
        if (useGravity)
            GetComponent<Rigidbody2D>().gravityScale = 1;
        else
            GetComponent<Rigidbody2D>().gravityScale = 0;

        if (useTrigger)
            GetComponent<Collider2D>().isTrigger = true;
        else
            GetComponent<Collider2D>().isTrigger = false;

        StartCoroutine(destroyIn(lifetime));
    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("Player"))
            DealDamage(other.gameObject.GetComponent<Health>());


        if (destroyOnImpact) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (!canDamage)
            currentDamageCooldown = Mathf.Clamp(currentDamageCooldown - Time.deltaTime, 0, damageCooldown);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            DealDamage(other.GetComponent<Health>());
        }
    }

    private void DealDamage(Health player) {
        if (canDamage) {    
            player.takeDamage(damage);
            currentDamageCooldown = damageCooldown;

        }
    }

    public IEnumerator destroyIn(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
