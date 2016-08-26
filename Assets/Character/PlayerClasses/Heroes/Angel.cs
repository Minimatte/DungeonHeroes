using UnityEngine;
using System.Collections;

public class Angel : Hero {

    public Angel() {
        level = 1;
        heroName = "Angel";
        headIcon = null;
        spriteName = "Wizard";
        offensiveSpell = typeof(RayThrust);
        defensiveSpell = typeof(Teleport);

        health = 30;
        mana = 30;
        power = 10;
        stamina = 0;

        movementSpeed = 4;
}
	
}
