using UnityEngine;
using System.Collections;

public class Teleport : DefensiveSpell {

    private PlayerMovement2D movement;

    public float range = 3f;
    public GameObject teleportEffect;

    protected override void ActivateSpell() {
        if (teleportEffect != null) {
            GameObject go = Instantiate(teleportEffect, transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform, true);
            Destroy(go, 3f);
        }

        transform.position += transform.right * range * movement.GetRightValue;
    }

    protected override void Init() {
        manaCost = GetComponent<Mana>().maxMana * 0.20f;
        movement = GetComponent<PlayerMovement2D>();
        spellName = "Teleport";
    }
}
