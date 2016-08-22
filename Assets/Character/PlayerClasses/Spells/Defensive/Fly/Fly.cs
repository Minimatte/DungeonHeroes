using UnityEngine;
using System.Collections;

public class Fly : DefensiveSpell {


    private PlayerMovement2D movement;

    protected override void Init() {
        passive = true;
        movement = GetComponent<PlayerMovement2D>();
        manaCost = GetComponent<Mana>().maxMana * 0.1f;
    }

    protected override void ActivateSpell() {
        movement.Jump(1, true);
    }

    void Update() {
        if (Input.GetButtonDown("Jump") && !movement.grounded && !movement.hasStamina) {
            UseSpell();
        }
    }

}
