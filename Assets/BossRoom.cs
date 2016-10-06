using UnityEngine;
using System.Collections;

public class BossRoom : MonoBehaviour {
    public delegate void CurrentBossDiesDelegate();

    public static GameObject currentBoss;
    public static CurrentBossDiesDelegate OnBossDeath;
    public GameObject ExitPortal;

    void Awake() {
        OnBossDeath = new CurrentBossDiesDelegate(SpawnExitPortal);
        
        ExitPortal.SetActive(false);
    }

    public void SpawnExitPortal() {
        ExitPortal.SetActive(true);
    }
}
