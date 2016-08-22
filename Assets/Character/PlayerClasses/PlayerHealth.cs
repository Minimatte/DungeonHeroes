using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlayerHealth : Health {

    public override void takeDamage(float damageToTake) {

        base.takeDamage(damageToTake);
    }


    public override void Kill() {
        if (PlayerHeroes.heroes.Count > 1) {
            Hero current = PlayerHeroes.currentHero;
            PlayerHeroes.NextHero();
            GetComponent<HeroClass>().Setup();
            PlayerHeroes.RemoveHero(current);
            UIManager.UpdateHeroIcons();
        } else {
            PlayerHeroes.RemoveHero(PlayerHeroes.currentHero);
            UIManager.UpdateHeroIcons();
            GameSaver.RemoveSaveGame();
            base.Kill();
        }
    }

}
