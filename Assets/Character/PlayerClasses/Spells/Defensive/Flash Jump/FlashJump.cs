using UnityEngine;
using System.Collections;

public class FlashJump : DefensiveSpell {


    private PlayerMovement2D movement;

    protected override void Init() {
        movement = GetComponent<PlayerMovement2D>();
        manaCost = 7;
        spellName = "FlashJump";
    }

    protected override void ActivateSpell() {
        movement.Jump(0.5f, true);
        GetComponent<Rigidbody2D>().AddForce(movement.jumpForce * transform.right * movement.GetRightValue, ForceMode2D.Impulse);
    }

    public override void UseSpell() {
            base.UseSpell();
    }
}
