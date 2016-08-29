using UnityEngine;
using System.Collections;

public class Wizard : Hero {

    public Wizard() {
        level = 1;
        heroName = "Wizard";
        headIcon = null;
        spriteName = "Wizard";
        offensiveSpell = typeof(Fireball);
        defensiveSpell = typeof(Teleport);

        health = 15;
        mana = 30;
        power = 25;
        stamina = 3;

        movementSpeed = 3;

        upgrade = Upgrade.Mana;
        upgradeAmount = 5;
    }

}
