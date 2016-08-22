using UnityEngine;
using System.Collections;

public class Knight : Hero {

    public Knight() {
        level = 1;
        heroName = "Knight";
        headIcon = null;
        spriteName = "Knight";
        offensiveSpell = typeof(Whirlwind);
        defensiveSpell = typeof(FlashJump);

        health = 50;
        mana = 10;
        power = 10;
        stamina = 3;
        movementSpeed = 3;
    }

}
