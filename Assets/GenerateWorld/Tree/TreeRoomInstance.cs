using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TreeRoomInstance : MonoBehaviour {

    public Room room;

    public bool startingRoom = false;
    public bool endroom = false;

    public GameObject EndPortal;

    public void Initialize() {
        if (startingRoom) {
            //SpawnRoomDifferences();
            SpawnTreasure();
        } else if (endroom) {
            SpawnPortal();
        } else {
            //SpawnRoomDifferences();
            SpawnTreasure();
            SpawnTraps();
            SpawnEnemies();
        }
    }

    private void SpawnRoomDifferences() {
        Instantiate(Resources.Load<GameObject>("JumpStickPrefab"), room.middle, Quaternion.identity);
        Instantiate(Resources.Load<GameObject>("JumpStickPrefab"), room.middle + new Vector2(0, 1), Quaternion.identity);
    }

    private void SpawnEnemies() {
        int amount = Random.Range(1, 4);

        var enemies = Resources.LoadAll("Enemies/", typeof(GameObject)).Cast<GameObject>();

        List<GameObject> e = new List<GameObject>();

        foreach (var go in enemies) {
            e.Add(go);
        }

        for (int i = 0; i < amount; i++) {

            Vector2 random = room.RandomPositionInRoom;

            Instantiate(e[Random.Range(0, e.Count)], random, Quaternion.identity);

        }
    }

    private void SpawnTreasure() {
        int amount = Random.Range(0, 3);

        for (int i = 0; i < amount; i++) {

            Vector2 random = room.RandomPositionInRoom;
            RaycastHit2D hit = Physics2D.Linecast(random, random + Vector2.down * room.height, 1 << LayerMask.NameToLayer("Ground"));
            Instantiate(Resources.Load<GameObject>("Chest"), hit.point, Quaternion.identity);


        }

    }

    private void SpawnTraps() {

    }

    private void SpawnPortal() {
        RaycastHit2D hit = Physics2D.Linecast(room.position, room.position + Vector2.down * room.height);
        Instantiate(Resources.Load<GameObject>("Portal"), hit.point, Quaternion.identity);
    }

}
