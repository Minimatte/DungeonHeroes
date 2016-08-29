using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameSaver : MonoBehaviour {

    public static void SaveGame() {

        SaveData data = new SaveData();
        GameObject player = GameEvents.player;
        HeroClass rc = player.GetComponent<HeroClass>();
        Hero playerStats = rc.hero;

        data.gold = PlayerItems.gold;
        data.level = playerStats.level;

        Type[] heroes = new Type[PlayerHeroes.heroes.Count];
        for (int i = 0; i < PlayerHeroes.heroes.Count; i++) {
            heroes[i] = PlayerHeroes.heroes[i].GetType();
        }
        data.hero = heroes;
        data.upgrades = PlayerHeroes.HeroUpgrades;
        data.unlockedDungeons = GameEvents.dungeonsCleared;

        WriteToFile(data);
    }

    private static void WriteToFile(SaveData data) {
        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");

        SaveData temp = new SaveData();

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/Saves/player.binary");

        temp = data;

        formatter.Serialize(saveFile, temp);
        saveFile.Close();
    }

    public static SaveData LoadData() {
        BinaryFormatter formatter = new BinaryFormatter();
        SaveData temp = null;
        if (File.Exists(Application.persistentDataPath + "/Saves/player.binary")) {

            FileStream saveFile = File.OpenRead(Application.persistentDataPath + "/Saves/player.binary");

            temp = (SaveData)formatter.Deserialize(saveFile);
            saveFile.Close();
        } else
            Debug.Log("Load file not found");
        return temp;
    }

    public static void RemoveSaveGame() {
        if (File.Exists(Application.persistentDataPath + "/Saves/player.binary")) {
            File.Delete(Application.persistentDataPath + "/Saves/player.binary");
        }
    }

}

[Serializable]
public class SaveData {
    public int gold { get; set; }
    public int level { get; set; }

    public Type[] hero { get; set; }

    public bool[] unlockedDungeons { get; set; }

    public Dictionary<Upgrade, int> upgrades {get;set;}
}