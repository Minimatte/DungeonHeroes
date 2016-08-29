using UnityEngine;
using System.Collections;

public class Knight : Hero {

    public Knight() {
        level = 1;
        heroName = "Knight";
        headIcon = null;
        spriteName = "Knight";
        offensiveSpell = typeof(Slash);
        defensiveSpell = typeof(Shield);

        health = 50;
        mana = 10;
        power = 10;
        stamina = 3;
        movementSpeed = 3;


        upgrade = Upgrade.Health;
        upgradeAmount = 10;
    }

}
