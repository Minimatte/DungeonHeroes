using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpellRandmizer {

    public Type[] offensiveSpells = { typeof(NinjaStar) };

    public Type[] defensiveSpells = { typeof(Fly) };

    public Type GetRandomOffensive() {
        return offensiveSpells[UnityEngine.Random.Range(0, offensiveSpells.Length)];
    }

    public Type GetRandomDefensive() {
        return defensiveSpells[UnityEngine.Random.Range(0, defensiveSpells.Length)];
    }
}
