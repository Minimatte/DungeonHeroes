using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Health : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;
    public float healthRegen = 0;
    public bool hasDamageFrames = false;
    public float damageFramesTime = 0.5f;
    public bool canTakeDamage = true;


    public List<GameObject> deathEffects;
    public List<AudioClip> deathSoundEffects;

    public virtual void TakeDamage(float damageToTake) {
        if (!canTakeDamage)
            return;

        currentHealth -= damageToTake;

        if (hasDamageFrames) {
            StartCoroutine(DamageFrames(damageFramesTime));
        }

        if (currentHealth <= 0) {
            if (deathEffects.Count > 0)
                foreach (GameObject go in deathEffects)
                    Instantiate(go, transform.position, Quaternion.identity);

            if (deathSoundEffects.Count > 0)
                foreach (AudioClip clip in deathSoundEffects)
                    AudioSource.PlayClipAtPoint(clip, transform.position);

            Kill();
        }
    }

    public virtual void Kill() {
        Destroy(gameObject);
    }

    public void Invulnerable(float time) {
        StartCoroutine(MakeInvulnerable(time));
    }

    private IEnumerator MakeInvulnerable(float time) {
        canTakeDamage = false;
        yield return new WaitForSeconds(time);
        canTakeDamage = true;
    }

    protected IEnumerator DamageFrames(float duration) {
        canTakeDamage = false;
        float elapsed = 0;

        var renderers = GetComponentsInChildren<SpriteRenderer>();

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            for (int i = 0; i < renderers.Length; i++) {
                if (renderers[i] != null)
                    renderers[i].enabled = !renderers[i].enabled;
            }

            yield return 0;
        }

        for (int i = 0; i < renderers.Length; i++) {
            if (renderers[i] != null)
                renderers[i].enabled = true;
        }

        canTakeDamage = true;

    }

    public virtual void heal(float healAmount) {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Update() {
        currentHealth += healthRegen * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void SetupHealth(int health) {
        maxHealth = health;
        currentHealth = maxHealth;

    }

}
