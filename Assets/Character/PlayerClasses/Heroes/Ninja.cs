using UnityEngine;
using System.Collections;

public class Ninja : Hero {

    public Ninja() {
        level = 1;
        heroName = "Ninja";
        headIcon = null;
        spriteName = "Ninja";
        offensiveSpell = typeof(NinjaStar);
        defensiveSpell = typeof(FlashJump);

        health = 35;
        mana = 20;
        power = 15;
        stamina = 5;

        movementSpeed = 4;


        upgrade = Upgrade.Stamina;
        upgradeAmount = 1;
    }

}
