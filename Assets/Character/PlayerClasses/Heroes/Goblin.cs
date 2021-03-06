﻿using UnityEngine;
using System.Collections;

public class Goblin : Hero {

    public Goblin() {
        level = 1;
        heroName = "Goblin";
        headIcon = null;
        spriteName = "Goblin";
        offensiveSpell = typeof(Bone);
        defensiveSpell = typeof(Berserk);

        health = 40;
        mana = 10;
        power = 15;
        stamina = 4;

        movementSpeed = 3;

        upgrade = Upgrade.Power;
        upgradeAmount = 7;

    }

}
