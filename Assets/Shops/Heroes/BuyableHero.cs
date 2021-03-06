﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuyableHero : Buyable {
    public bool debug = false;
    public Hero hero;

    void Awake() {
        hero = RandomizeHero();
        if (!debug)
            SetCost();
        SetSprites();
    }

    private void SetCost() {
        if (PlayerItems.gold <= 63)
            cost = (int)(63 * UnityEngine.Random.Range(0.40f, 1.40f)) + (int)(PlayerItems.gold * UnityEngine.Random.Range(0.6f, 1.2f));
        else
            cost = (int)(PlayerItems.gold * UnityEngine.Random.Range(0.6f, 1.2f));
    }

    public Hero RandomizeHero() {

        List<Type> heroes = new List<Type>();

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (var type in asm.GetTypes()) {
                if (type.IsSubclassOf(typeof(Hero)))
                    heroes.Add(type);
            }
        }

        var randomHero = heroes[UnityEngine.Random.Range(0, heroes.Count)];

        var hero = (Hero)Activator.CreateInstance(randomHero);
        return hero;

    }

    private void SetSprites() {
        var subsprites = Resources.LoadAll<Sprite>("Characters/" + hero.spriteName);
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) {
            string spritename = renderer.gameObject.name;
            var newSprite = Array.Find(subsprites, item => item.name == spritename);
            hero.headIcon = Array.Find(subsprites, item => item.name == "Head");
            if (newSprite)
                renderer.sprite = newSprite;
            else
                renderer.sprite = null;
        }
    }

    private void BuyHero() {
        if (PlayerHeroes.FullHeroes) {
            if (PlayerHeroes.HeroUpgrades == null)
                PlayerHeroes.HeroUpgrades = new Dictionary<Upgrade, int>();

            if (!PlayerHeroes.HeroUpgrades.ContainsKey(hero.upgrade))
                PlayerHeroes.HeroUpgrades.Add(hero.upgrade, hero.upgradeAmount);
            else
                PlayerHeroes.HeroUpgrades[hero.upgrade] += hero.upgradeAmount;
        } else {
            PlayerHeroes.AddHero(hero);
        }
        Destroy(gameObject);
    }

    public override void Buy() {
        BuyHero();
    }
}
