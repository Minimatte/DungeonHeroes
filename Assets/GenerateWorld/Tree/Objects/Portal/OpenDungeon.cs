using UnityEngine;
using System.Collections;
using System;

public class OpenDungeon : Buyable {
    public override void Buy() {
        GetComponent<Portal>().open = true;
        GameEvents.dungeonsCleared[GetComponent<Portal>().dungeonID] = true;
        GameSaver.SaveGame();
    }
}
