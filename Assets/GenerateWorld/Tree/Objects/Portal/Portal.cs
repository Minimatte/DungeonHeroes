using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public string LevelName = "Town";
    public bool open = true;
    public int dungeonID = 0;
    private GameObject gameEvents;

    void Start() {
        if (GameEvents.dungeonsCleared[dungeonID])
            open = true;
        else
            open = false;
    }

    public void UsePortal() {
        GameSaver.SaveGame();
        GameEvents.ChangeLevel(LevelName);
        
    }
}
