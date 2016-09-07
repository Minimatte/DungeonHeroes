using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlayerHealth : Health {
    public AudioClip takeDamageSound;

    public override void TakeDamage(float damageToTake) {
        if (damageToTake >= 0 && canTakeDamage) {
            Camera.main.GetComponent<CameraMovement2D>().ShakeCamera(0.2f, 1);
            if (takeDamageSound != null)
                AudioSource.PlayClipAtPoint(takeDamageSound, transform.position);
        }
        base.TakeDamage(damageToTake);
    }

    public override void Kill() {
        if (PlayerHeroes.heroes.Count > 1) {
            Hero current = PlayerHeroes.currentHero;
            PlayerHeroes.NextHero();
            GetComponent<HeroClass>().Setup();
            PlayerHeroes.RemoveHero(current);
            UIManager.UpdateHeroIcons();
            GameSaver.SaveGame();
        } else {
            PlayerHeroes.RemoveHero(PlayerHeroes.currentHero);
            UIManager.UpdateHeroIcons();
            GameSaver.RemoveSaveGame();
            base.Kill();
        }
    }

}
