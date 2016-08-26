using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class RemoveData : MonoBehaviour {

    [MenuItem("Data/Saves/Remove Current Save")]
    public static void RemoveSaveGame() {
        if (File.Exists(Application.persistentDataPath + "/Saves/player.binary")) {
            File.Delete(Application.persistentDataPath + "/Saves/player.binary");
        }
    }

    [MenuItem("Data/Saves/Save Game")]
    public static void SaveGame() {
        if (Application.isPlaying)
            GameSaver.SaveGame();
    }

    [MenuItem("Data/Player/Heal To Full")]
    public static void HealToFull() {
        if (Application.isPlaying)
            GameEvents.player.GetComponent<Health>().heal(int.MaxValue);
    }

    [MenuItem("Data/Player/Infinite Coins")]
    public static void GainCoins() {
        if (Application.isPlaying)
            PlayerItems.gold += 1000;
    }


}
