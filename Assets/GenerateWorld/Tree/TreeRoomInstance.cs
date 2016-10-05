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

    public List<GameObject> Props;
    public int maxProps = 0;
    public float ChanceToSpawnProps = 0;

    public List<GameObject> RoofDeco;
    public int maxRoofDeco = 0;
    public float ChanceToSpawnRoofDeco = 0;

    public List<GameObject> Enemies;
    public Vector2 EnemiesMinMax = Vector2.zero;

    public void Initialize() {
        if (startingRoom) {
            SpawnTreasure();

            if (CheckForPropChance())
                SpawnProps();

        } else if (endroom) {
            SpawnPortal();
        } else {
            SpawnTreasure();
            if (CheckForTrapChance())
                SpawnTraps(maxTraps);

            if (CheckForPropChance())
                SpawnProps();

            if (CheckForRoofDecoChance() && RoofDeco.Count > 0)
                SpawnRoofDeco();

            SpawnEnemies((int)EnemiesMinMax.x, (int)EnemiesMinMax.y);
        }
    }

    private bool CheckForTrapChance() {
        if (Traps == null || Traps.Count == 0)
            return false;

        if (Random.Range(0, 100) < ChanceToSpawnTraps)
            return true;
        else
            return false;
    }

    private bool CheckForPropChance() {
        if (Props == null || Props.Count == 0)
            return false;

        if (Random.Range(0, 100) < ChanceToSpawnProps)
            return true;
        else
            return false;
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

    private void SpawnProps() {

        int propAmount = 0;

        do {
            Vector2 random = room.RandomPositionInRoom;
            RaycastHit2D hit = Physics2D.Linecast(random, random + Vector2.down * room.height, 1 << LayerMask.NameToLayer("Ground"));

            if (hit.collider.CompareTag("Object"))
                continue;

            propAmount++;
            Instantiate(Props[Random.Range(0, Props.Count)], hit.point, Quaternion.identity);
        } while (CheckForPropChance() && propAmount < maxProps);
    }

    private void SpawnPortal() {
        RaycastHit2D hit = Physics2D.Linecast(room.position, room.position + Vector2.down * room.height);
        Instantiate(Resources.Load<GameObject>("Portal"), hit.point, Quaternion.identity);
    }

    private void SpawnRoofDeco() {

        int decoAmount = 0;

        do {
            Vector2 random = room.RandomPositionInRoom;
            RaycastHit2D hit = Physics2D.Linecast(random, random + Vector2.up * room.height, 1 << LayerMask.NameToLayer("Ground"));

            if (hit.collider.CompareTag("Object"))
                continue;

            decoAmount++;
            Instantiate(RoofDeco[Random.Range(0, RoofDeco.Count)], hit.point, Quaternion.identity);
        } while (CheckForRoofDecoChance() && decoAmount < maxRoofDeco);
    }

    private bool CheckForRoofDecoChance() {
        if (Props == null || Props.Count == 0)
            return false;

        if (Random.Range(0, 100) < ChanceToSpawnRoofDeco)
            return true;
        else
            return false;
    }
}
