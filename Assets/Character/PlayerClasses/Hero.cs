﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Hero {

    public string heroName = "NONE";
    public string spriteName = "base";
    public Sprite headIcon;

    public Type offensiveSpell;
    public Type defensiveSpell;

    public int health;
    public int mana;
    public int power;
    public int level;
    public int stamina;

    public bool infiniteJumps = false;

    public float movementSpeed = 2f;

    public Upgrade upgrade;
    public int upgradeAmount;
}
