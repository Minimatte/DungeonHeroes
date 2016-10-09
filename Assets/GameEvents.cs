using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class GameEvents : MonoBehaviour {

    private static GameEvents instance;
    public static bool[] dungeonsCleared = new bool[10];
    public static GameObject player;
    public GameObject baseItem;

    [Header("Randomize Options")]

    public List<string> sprites;
    public int statPoints = 15;
    private static bool hasRandmized = false;
    private static bool changingLevel = false;
    private static string LevelName = "Town";

    void Awake() {
        if (dungeonsCleared[0] == false)
            dungeonsCleared[0] = true;

        if (instance == null || instance == this) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else {
            Destroy(gameObject);
        }

        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");

        }

        if (!hasRandmized) {
            //RandomizePlayer();
            RandomizeHero();
            hasRandmized = true;
        }
    }

    public void RandomizeHero() {

        SaveData data = GameSaver.LoadData();

        if (data != null) {

            for (int i = 0; i < data.hero.Length; i++) {
                var hero = (Hero)Activator.CreateInstance(data.hero[i]);

                var subsprites = Resources.LoadAll<Sprite>("Characters/" + hero.spriteName);
                hero.headIcon = Array.Find(subsprites, item => item.name == "Head");

                PlayerHeroes.AddHero(hero);
            }
            PlayerHeroes.SetCurrentHero(PlayerHeroes.heroes[0]);


            PlayerItems.gold = data.gold;
            dungeonsCleared = data.unlockedDungeons;
            PlayerHeroes.HeroUpgrades = new Dictionary<Upgrade, int>();
            PlayerHeroes.HeroUpgrades = data.upgrades;
            print("Loaded player");

        } else {
            List<Type> heroes = new List<Type>();

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in asm.GetTypes()) {
                    if (type.IsSubclassOf(typeof(Hero)))
                        heroes.Add(type);
                }
            }


            var randomHero = heroes[UnityEngine.Random.Range(0, heroes.Count)];

            var hero = (Hero)Activator.CreateInstance(randomHero);
            PlayerHeroes.HeroUpgrades = new Dictionary<Upgrade, int>();
            PlayerHeroes.AddHero(hero);
            PlayerHeroes.SetCurrentHero(hero);
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        else
            player.GetComponent<HeroClass>().Setup();

        UIManager.UpdateHeroIcons();
    }

    public void SpawnItem(Item item, Vector3 position) {
        ItemComponent temp = (Instantiate(baseItem, position, Quaternion.identity) as GameObject).GetComponent<ItemComponent>();
        temp.item = item;
        temp.setItemSprite(item.sprite);
    }

    void Update() {
        if (changingLevel) {
            VignetteAndChromaticAberration vign = Camera.main.GetComponent<VignetteAndChromaticAberration>();
            if (vign.intensity < 1) {
                vign.intensity = Mathf.Clamp(vign.intensity + Time.deltaTime * 2, 0, 1);

            } else {
                SceneManager.LoadScene(LevelName);
                transform.position = Vector2.one;
                changingLevel = false;
            }

        } else { // not changing level
            VignetteAndChromaticAberration vign = Camera.main.GetComponent<VignetteAndChromaticAberration>();

            if (vign.intensity > 0) {
                vign.intensity = Mathf.Clamp(vign.intensity - Time.deltaTime * 2, 0, 1);
            }
        }
    }

    public static void ChangeLevel(string levelName) {
        LevelName = levelName;
        changingLevel = true;
    }

}
