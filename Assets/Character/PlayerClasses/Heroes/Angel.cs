using UnityEngine;
using System.Collections;

public class Angel : Hero {

    public Angel() {
        level = 1;
        heroName = "Angel";
        headIcon = null;
        spriteName = "Angel";
        offensiveSpell = typeof(RayThrust);
        defensiveSpell = typeof(AngelShield);

        health = 30;
        mana = 30;
        power = 10;
        stamina = 0;

        movementSpeed = 3;

        upgrade = Upgrade.Health;
        upgradeAmount = 5;
    }
	
}
