using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TreeRoomInstance : MonoBehaviour {

    public Room room;

    public bool startingRoom = false;
    public bool endroom = false;

    public GameObject EndPortal;

    public List<GameObject> Traps;
    public int maxTraps = 0;
    public float ChanceToSpawnTraps = 0;

    public List<GameObject> Enemies;
    public Vector2 EnemiesMinMax = Vector2.zero;

    public void Initialize() {
        if (startingRoom) {
            //SpawnRoomDifferences();
            SpawnTreasure();
        } else if (endroom) {
            SpawnPortal();
        } else {
            //SpawnRoomDifferences();
            SpawnTreasure();
            if (CheckForTrapChance())
                SpawnTraps(maxTraps);
            SpawnEnemies((int)EnemiesMinMax.x, (int)EnemiesMinMax.y);
        }
    }

    private bool CheckForTrapChance() {
        if (Random.Range(0, 100) < ChanceToSpawnTraps)
            return false;
        else
            return true;
    }

    private void SpawnRoomDifferences() {
        Instantiate(Resources.Load<GameObject>("JumpStickPrefab"), room.middle, Quaternion.identity);
        Instantiate(Resources.Load<GameObject>("JumpStickPrefab"), room.middle + new Vector2(0, 1), Quaternion.identity);
    }

    private void SpawnEnemies(int min, int max) {
        int amount = Random.Range(min, max);

        for (int i = 0; i < amount; i++) {

            Vector2 random = room.RandomPositionInRoom;

            Instantiate(Enemies[Random.Range(0, Enemies.Count)], random, Quaternion.identity);

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

    private void SpawnTraps(int max) {

        int trapAmount = 0;

        do {
            Vector2 random = room.RandomPositionInRoom;
            RaycastHit2D hit = Physics2D.Linecast(random, random + Vector2.down * room.height, 1 << LayerMask.NameToLayer("Ground"));

            if (hit.collider.CompareTag("Object"))
                continue;

            trapAmount++;
            Instantiate(Traps[Random.Range(0, Traps.Count)], hit.point, Quaternion.identity);
        } while (CheckForTrapChance() && trapAmount < max);
    }

    private void SpawnPortal() {
        RaycastHit2D hit = Physics2D.Linecast(room.position, room.position + Vector2.down * room.height);
        Instantiate(Resources.Load<GameObject>("Portal"), hit.point, Quaternion.identity);
    }

}
