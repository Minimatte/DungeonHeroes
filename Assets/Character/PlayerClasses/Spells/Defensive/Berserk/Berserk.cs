using UnityEngine;
using System.Collections;

public class Berserk : DefensiveSpell {

    bool isBererking = false;

    GameObject effect;
    GameObject effectInstance;
    PlayerHealth health;
    Mana mana;

    protected override void ActivateSpell() {
        isBererking = !isBererking;

        if (isBererking) {
            effectInstance = Instantiate(effect, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
            effectInstance.transform.SetParent(transform);
        } else {
            if (!health.canTakeDamage)
                health.canTakeDamage = true;
            Destroy(effectInstance);
        }

    }

    protected override void Init() {
        spellName = "Berserk";
        manaCost = 1;
        health = GetComponent<PlayerHealth>();
        effect = Resources.Load<GameObject>("BerserkVisuals");
    }

    void Update() {
        if (isBererking) {
            health.currentHealth -= Time.deltaTime;
            health.canTakeDamage = false;
        }
    }
}
