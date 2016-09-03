using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class PlayerHeroes {

    public const int MaxHeroes = 4;
    public static Hero currentHero;
    public static List<Hero> heroes = new List<Hero>();
    public static bool FullHeroes { get { return heroes.Count == MaxHeroes; } }

    public static Dictionary<Upgrade, int> HeroUpgrades;

    public static int GetPlayerPower { get { if (HeroUpgrades.ContainsKey(Upgrade.Power)) return currentHero.power + HeroUpgrades[Upgrade.Power]; else return currentHero.power; } }

    public static void NextHero() {
        var index = heroes.IndexOf(currentHero);

        if (index + 1 >= heroes.Count)
            SetCurrentHero(heroes[0]);
        else
            SetCurrentHero(heroes[index + 1]);
    }

    public static void AddHero(Hero hero) {
        if (heroes.Count >= MaxHeroes) {
            Debug.LogError("Reached max amount of heroes, make some sort of UI representation");
            return;
        }

        heroes.Add(hero);
        UIManager.CreateHeroIcon(hero.headIcon, hero);
    }

    public static Hero SetCurrentHero(Hero hero) {
        var returnThis = currentHero;
        currentHero = hero;
        UIManager.SetActiveHeroIcon(currentHero);
        return returnThis;
    }

    public static void RemoveHero(Hero hero) {
        var hi = UIManager.heroIcons[hero];
        GameEvents.Destroy(hi.gameObject);
        heroes.Remove(hero);
        GameSaver.SaveGame();
    }

    public override string ToString() {
        return currentHero.heroName;
    }

}
