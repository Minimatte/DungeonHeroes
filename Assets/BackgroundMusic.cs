using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip townMusic;
    public AudioClip world1;

    void Awake() {

        switch (SceneManager.GetActiveScene().name) {
            case "Town":
                GetComponent<AudioSource>().clip = townMusic;
                break;
            case "Dungeon":
                GetComponent<AudioSource>().clip = world1;
                break;
            default:
                GetComponent<AudioSource>().clip = townMusic;
                break;
        }
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(gameObject);
    }
}
