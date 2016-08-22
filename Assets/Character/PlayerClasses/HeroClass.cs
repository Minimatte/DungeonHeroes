using UnityEngine;
using System.Collections;
using System;

public class HeroClass : MonoBehaviour {

    public Hero hero;

    public string spriteSheetName;

    private static HeroClass instance;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Update() {
        if (Input.GetButtonDown("ChangeHero")) {
            if (PlayerHeroes.heroes.IndexOf(PlayerHeroes.currentHero) + (int)Input.GetAxisRaw("ChangeHero") < 0)
                 PlayerHeroes.SetCurrentHero(PlayerHeroes.heroes[PlayerHeroes.heroes.Count - 1]);
            else if (PlayerHeroes.heroes.IndexOf(PlayerHeroes.currentHero) + (int)Input.GetAxisRaw("ChangeHero") >= PlayerHeroes.heroes.Count)
                 PlayerHeroes.SetCurrentHero(PlayerHeroes.heroes[0]);
            else
                 PlayerHeroes.SetCurrentHero(PlayerHeroes.heroes[PlayerHeroes.heroes.IndexOf(PlayerHeroes.currentHero) + (int)Input.GetAxisRaw("ChangeHero")]);

            Setup();
        }
    }

    public void Setup() {
        

        Health hp = GetComponent<Health>();
        Mana mana = GetComponent<Mana>();

        var hpp = hp.currentHealth / hp.maxHealth;
        var mpp = mana.currentMana / mana.maxMana;

        if (hpp <= 0)
            hpp = 1;
        if (mpp <= 0)
            mpp = 1;

        hero = PlayerHeroes.currentHero;
        hp.maxHealth = hero.health;
        mana.maxMana = hero.mana;
        hp.currentHealth = hero.health * hpp;
        mana.currentMana = hero.mana * mpp;
        spriteSheetName = hero.spriteName;

        PlayerMovement2D movement = GetComponent<PlayerMovement2D>();

        movement.stamina = hero.stamina;

        if (movement.currentStamina > hero.stamina)
            movement.currentStamina = hero.stamina;

        SetSprites();

        GetComponent<SpellHandler>().UpdateSpells();
    }

    private void SetSprites() {
        var subsprites = Resources.LoadAll<Sprite>("Characters/" + hero.spriteName);
        hero.headIcon = Array.Find(subsprites, item => item.name == "Head");

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) {

            string spritename = renderer.gameObject.name;
            var newSprite = Array.Find(subsprites, item => item.name == spritename);

            if (newSprite)
                renderer.sprite = newSprite;
            else
                renderer.sprite = null;

        }
    }
}
