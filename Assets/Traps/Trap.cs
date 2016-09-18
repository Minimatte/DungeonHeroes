using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    public LayerMask hitmask;
    public Animator anim;
    public float cooldown = 1;
    protected float currentCooldown = 0;

    public AudioClip triggerSound;

    public float damage = 5;

    void Awake() {
        if (anim == null) {
            anim = GetComponentInChildren<Animator>();
        }
    }


    void Update() {
        if (currentCooldown > 0)
            currentCooldown = Mathf.Clamp(currentCooldown - Time.deltaTime, 0, float.MaxValue);

    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (currentCooldown > 0)
            return;

        if (anim != null && hitmask == (hitmask | (1 << collision.gameObject.layer))) {
            anim.SetTrigger("Trigger");
            currentCooldown = cooldown;
        }
    }

    public void PlayTrapSound() {
        if (triggerSound != null)
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
    }

    public virtual void DealDamage() {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + Vector3.up * 0.16f, Vector2.one, 90, hitmask);

        PlayTrapSound();

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].GetComponent<Health>()) {
                hits[i].GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.16f, Vector3.one);
    }
}
