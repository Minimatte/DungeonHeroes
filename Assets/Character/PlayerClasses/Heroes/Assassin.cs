using UnityEngine;
using System.Collections;

public class Assassin : Hero {

    public Assassin() {
        level = 1;
        heroName = "Assassin";
        headIcon = null;
        spriteName = "Assassin";
        offensiveSpell = typeof(Backstab);
        defensiveSpell = typeof(Shadowstep);

        health = 20;
        mana = 5;
        power = 35;
        stamina = 3;

        movementSpeed = 5;
    }

}
