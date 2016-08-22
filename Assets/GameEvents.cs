using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

public class GameEvents : MonoBehaviour {

    private static GameEvents instance;
    public static bool[] dungeonsCleared = new bool[10];
    public static GameObject player;
    public GameObject baseItem;

    [Header("Randomize Options")]

    public List<string> sprites;
    public int statPoints = 15;
    private static bool hasRandmized = false;

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

            PlayerHeroes.AddHero(hero);
            PlayerHeroes.SetCurrentHero(hero);
        }
        player.GetComponent<HeroClass>().Setup();

        UIManager.UpdateHeroIcons();
    }

    public void SpawnItem(Item item, Vector3 position) {
        ItemComponent temp = (Instantiate(baseItem, position, Quaternion.identity) as GameObject).GetComponent<ItemComponent>();
        temp.item = item;
        temp.setItemSprite(item.sprite);
    }

    public void RandomizePlayer() {


        SaveData data = GameSaver.LoadData();

        Stats playerStats = new Stats();
        string sprite = "Base";

        if (data != null) {

            //sprite = data.sprite;

            //playerStats.baseHealth = data.hp;
            //playerStats.baseMana = data.mana;
            //playerStats.basePower = data.power;
            //playerStats.expGrantedFromKill = 1;
            //playerStats.movementSpeed = data.movementSpeed;
            //playerStats.currentLevel = data.level;

            //player.AddComponent(data.offensiveAbility);
            //player.AddComponent(data.defensiveAbility);

            PlayerItems.gold = data.gold;
            print("Loaded player");

        } else {


            sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];

            int statLeft = statPoints;

            int hp = 1;
            int mana = 1;
            int power = 1;

            while (statLeft > 0) {
                int random = UnityEngine.Random.Range(0, 3);
                switch (random) {
                    case 0:
                        hp++;
                        break;
                    case 1:
                        mana++;
                        break;
                    case 2:
                        power++;
                        break;
                }
                statLeft--;
            }


            print("hp:" + hp + " mana:" + mana + " power:" + power);


            playerStats.baseHealth = (int)StatMultipliers.health * hp;
            playerStats.baseMana = (int)StatMultipliers.mana * mana;
            playerStats.basePower = (int)StatMultipliers.power * power;
            playerStats.expGrantedFromKill = 1;
            playerStats.movementSpeed = 4;
            playerStats.currentLevel = 1;

            SpellRandmizer SR = new SpellRandmizer();

            player.AddComponent(SR.GetRandomOffensive());
            player.AddComponent(SR.GetRandomDefensive());

        }


        RandomizedClass pc = player.GetComponent<RandomizedClass>();
        pc.playerStats = playerStats;
        pc.spriteSheetName = sprite;
        pc.Setup();



    }



}
