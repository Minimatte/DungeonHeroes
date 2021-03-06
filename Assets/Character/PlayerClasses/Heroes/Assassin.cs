﻿using UnityEngine;
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
        power = 10;
        stamina = 4;

        movementSpeed = 5;

        upgrade = Upgrade.Power;
        upgradeAmount = 10;
    }

}
