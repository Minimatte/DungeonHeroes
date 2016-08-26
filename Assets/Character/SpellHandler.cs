using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class SpellHandler : MonoBehaviour {

    public OffensiveSpell offensive;
    public DefensiveSpell defensive;
    public bool canStrafe = true;

    void Start() {
        UpdateSpells();
    }


    public void UpdateSpells() {

        foreach (Spell s in gameObject.GetComponents<Spell>())
            Destroy(s);

        gameObject.AddComponent(PlayerHeroes.currentHero.offensiveSpell);
        gameObject.AddComponent(PlayerHeroes.currentHero.defensiveSpell);

        offensive = GetComponent<OffensiveSpell>();
        defensive = GetComponent<DefensiveSpell>();

        UIManager.UpdateSpellIcons();
    }

    void Update() {
        offensive = GetComponent<OffensiveSpell>();
        defensive = GetComponent<DefensiveSpell>();
        UIManager.UpdateSpellIcons();
        if (Input.GetButtonDown("OffensiveSpell") && !offensive.passive) {
            offensive.UseSpell();
            GetComponent<Animator>().SetTrigger("Attack");
            if (canStrafe)
                GetComponent<PlayerMovement2D>().strafeCooldown = 0.3f;
        }

        if (Input.GetButtonDown("DefensiveSpell") && !defensive.passive) {
            defensive.UseSpell();
        }
    }



}
