using UnityEngine;
using System.Collections;

public class Heal : DefensiveSpell {

    protected override void Init() {
        manaCost = 5;
    }

    protected override void ActivateSpell() {
        Health hp = GetComponent<Health>();
        RandomizedClass pc = GetComponent<RandomizedClass>();
        hp.heal(pc.playerStats.basePower);

        print(manaCost);

    }
}
