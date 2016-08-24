using UnityEngine;
using System.Collections;

public class Goblin : Hero {

    public Goblin() {
        level = 1;
        heroName = "Goblin";
        headIcon = null;
        spriteName = "Goblin";
        offensiveSpell = typeof(NinjaStar);
        defensiveSpell = typeof(FlashJump);

        health = 40;
        mana = 10;
        power = 25;
        stamina = 3;


        movementSpeed = 4;
}
	
}
