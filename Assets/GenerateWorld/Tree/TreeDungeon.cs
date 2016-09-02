using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TreeDungeon : MonoBehaviour {

    [Header("GameObjects")]
    public GameObject player;
    public GameObject gameEvents;
    public GameObject jumpBoard;

    [Header("Tiles")]
    public GameObject floorTile;
    public GameObject wallRight;
    public GameObject WallLeft;
    public GameObject roofTile;
    public GameObject[] wallTile;
    [Space(5)]
    public GameObject bottomLeft;
    public GameObject bottomRight;
    public GameObject topLeft;
    public GameObject topRight;

    [Header("Corridor Tiles")]
    public GameObject corridorLeft;
    public GameObject corridorRight;

    [Header("Room Options")]
    Room root;

    [Header("Enemies")]
    public List<GameObject> Enemies;
    public Vector2 EnemiesMinMax;

    [Header("Traps")]
    public List<GameObject> Traps;
    public int maxTraps = 0;
    [Range(0, 100)]
    public float ChanceToSpawnTraps = 0;

    [Header("Props")]
    public List<GameObject> Props;
    public int maxProps = 0;
    [Range(0, 100)]
    public float ChanceToSpawnProps = 0;

    [Header("Foreground")]
    public List<GameObject> Foreground;
    [Range(0, 10)]
    public float ChanceToSpawnForeground = 0;

    [Header("Room Options")]
    public Vector2 roomMinSize, roomMaxSize;

    public int corridorSize = 3;
    public int maxDistance;
    public int minDistance;

    [Range(0, 100)]
    public int iterations = 10;

    List<Room> roomList;

    void Start() {
        roomList = new List<Room>();
        root = new Room(0, 0, roomMinSize, roomMaxSize, this);
        roomList.Add(root);

        GameObject go = new GameObject("Root");
        RoomToWorld(root, go);
        TreeRoomInstance instance = go.AddComponent<TreeRoomInstance>();
        instance.room = root;
        root.instance = instance;
        SetInstanceOptions(ref instance);

        int counter = 0;

        PopulateDungeon(ref counter, root);
        MakeCorridors(root);
        FillWorld();
        ClearRooms();
        ClearRooms();
        ClearRooms();

        if (GameEvents.player == null) {
            print("Gameevents player is null. Probably started game in the wrong scene!");

            GameObject p = Instantiate(player, root.position, Quaternion.identity) as GameObject;
            (Instantiate(gameEvents) as GameObject).GetComponent<GameEvents>().RandomizeHero();

            GameEvents.player = p;
            Camera.main.GetComponent<CameraMovement2D>().player = p;

            p.GetComponent<SpellHandler>().enabled = true;
        }

        GameEvents.player.transform.position = root.position;

        roomList[roomList.Count - 1].instance.endroom = true;
        roomList[0].instance.startingRoom = true;

        for (int i = 0; i < roomList.Count; i++) {

            roomList[i].instance.Initialize();
        }

    }

    private void FillWorld() {

        var maxX = roomList.Max(obj => obj.x + obj.width);
        var maxY = roomList.Max(obj => obj.y + obj.height);
        var minX = roomList.Min(obj => obj.x);
        var minY = roomList.Min(obj => obj.y);

        var avgX = roomList.Average(obj => obj.width);
        var avgY = roomList.Average(obj => obj.height);

        GameObject go = new GameObject();
        go.name = "backgroundWorld";

        for (int x = (int)minX - (int)avgX; x < maxX + avgX; x++) {
            for (int y = (int)minY - (int)avgY; y < maxY + avgY; y++) {
                (Instantiate(wallTile[Random.Range(0, wallTile.Length)], new Vector3(x, y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);

                if (Random.Range(0, 100) < ChanceToSpawnForeground) {
                    (Instantiate(Foreground[Random.Range(0, Foreground.Count)], new Vector3(x, y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);

                }

            }
        }
    }

    private void PopulateDungeon(ref int counter, Room room) {
        counter += 1;
        if (counter > iterations) {
            return;
        }

        Room newRoom = AddToList(room, counter);
        Room newRoom2 = AddToList(room, counter);

        if (newRoom != null)
            PopulateDungeon(ref counter, newRoom);
        if (newRoom2 != null)
            PopulateDungeon(ref counter, newRoom2);

    }

    private Room AddToList(Room r, int counter) {
        Room newRoom = r.MakeNeighbours((int)roomMinSize.x, (int)roomMinSize.y, (int)roomMaxSize.x, (int)roomMaxSize.y, maxDistance, minDistance);
        if (newRoom != null) {

            roomList.Add(newRoom);
            var go = new GameObject(newRoom.ToString() + ":" + counter);
            go.transform.position = newRoom.position;

            TreeRoomInstance instance = go.AddComponent<TreeRoomInstance>();

            instance.room = newRoom;

            RoomToWorld(newRoom, go);

            newRoom.instance = instance;

            SetInstanceOptions(ref instance);

            return newRoom;
        }
        return null;
    }

    private void SetInstanceOptions(ref TreeRoomInstance instance) {
        instance.ChanceToSpawnTraps = ChanceToSpawnTraps;
        instance.maxTraps = maxTraps;
        instance.Enemies = Enemies;
        instance.Traps = Traps;
        instance.EnemiesMinMax = EnemiesMinMax;
        instance.Props = Props;
        instance.maxProps = maxProps;
        instance.ChanceToSpawnProps = ChanceToSpawnProps;

    }


    private void RoomToWorld(Room newRoom, GameObject go) {
        for (int x = 0; x < newRoom.width; x++) {
            for (int y = 0; y < newRoom.height; y++) {
                if (x == 0) { // left wall

                    if (y == 0)
                        (Instantiate(bottomLeft, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);
                    else if (y == newRoom.height - 1)
                        (Instantiate(topLeft, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);
                    else
                        (Instantiate(WallLeft, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);

                } else if (x == newRoom.width - 1) { // right wall

                    if (y == 0)
                        (Instantiate(bottomRight, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);
                    else if (y == newRoom.height - 1)
                        (Instantiate(topRight, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);
                    else
                        (Instantiate(wallRight, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);


                } else if (y == newRoom.height - 1) { // roof
                    (Instantiate(roofTile, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);
                } else if (y == 0) // floor
                    (Instantiate(floorTile, new Vector3(newRoom.x + x, newRoom.y + y), Quaternion.identity) as GameObject).transform.SetParent(go.transform);
            }
        }
    }

    private void ClearRooms() {
        foreach (Room room in roomList) {

            Collider2D[] hits = Physics2D.OverlapAreaAll(room.bottomLeft + Vector2.one, room.topRight - Vector2.one * 2);

            foreach (Collider2D hit in hits) {
                if (!hit.gameObject.GetComponent<HeroClass>())
                    Destroy(hit.gameObject);
            }

        }


        foreach (Room r in roomList) {
            CleanupCorridors(r, r.rightRoom);
            CleanupCorridors(r, r.leftRoom);
        }
    }


    private void CleanupCorridors(Room a, Room b) {
        if (a == null || b == null) {
            return;
        }

        var direction = (b.position - a.position);
        var cSize = Random.Range(1, corridorSize + 1);

        var multip = direction.x >= 0 ? 1 : -1;

        for (int x = 0; x < Mathf.Abs(direction.x); x++) {
            for (int y = -cSize; y <= cSize; y++) {
                if (y == -cSize || y == cSize) { } else {
                    Collider2D[] hits = Physics2D.OverlapPointAll(a.position + new Vector2(x, y) * multip);
                    foreach (Collider2D hit in hits)
                        if (hit != null && !hit.CompareTag("Object"))
                            Destroy(hit.gameObject);
                }
            }
        }

        multip = direction.y >= 0 ? 1 : -1;

        for (int y = 0; y < Mathf.Abs(direction.y); y++) {
            for (int x = -cSize; x <= cSize; x++) {
                if (x == -cSize || x == cSize) {

                } else {
                    Collider2D hit = Physics2D.OverlapPoint(a.position + new Vector2(x + direction.x * multip, y) * multip);
                    if (hit != null && !hit.CompareTag("Object"))
                        Destroy(hit.gameObject);
                }
            }
        }
    }

    private void MakeCorridors(Room room) {

        if (room == null) {
            return;
        }


        MakeCorridorBetween(room, room.leftRoom);
        MakeCorridorBetween(room, room.rightRoom);

        MakeCorridors(room.rightRoom);
        MakeCorridors(room.leftRoom);
    }

    private void MakeCorridorBetween(Room a, Room b) {

        if (a == null || b == null)
            return;

        var direction = (b.position - a.position);

        var multip = direction.x >= 0 ? 1 : -1;


        for (int x = 0; x < Mathf.Abs(direction.x) + corridorSize + 1; x++) {
            for (int y = -corridorSize; y <= corridorSize; y++) {
                if (y == -corridorSize || y == corridorSize) {
                    //Instantiate(floorTile, a.position + new Vector2(x, y) * multip, Quaternion.identity);
                } else {
                    Collider2D hit = Physics2D.OverlapPoint(a.position + new Vector2(x, y) * multip);
                    if (hit != null && !hit.CompareTag("Object"))
                        Destroy(hit.gameObject);
                }
            }
        }

        multip = direction.y >= 0 ? 1 : -1;

        for (int y = 0; y < Mathf.Abs(direction.y); y++) {
            for (int x = -corridorSize; x <= corridorSize; x++) {
                if (x == -corridorSize || x == corridorSize) {
                    //Instantiate(wallTile[Random.Range(0, wallTile.Length)], a.position + new Vector2(x + direction.x * multip, y) * multip, Quaternion.identity);
                    continue;
                } else {
                    Collider2D hit = Physics2D.OverlapPoint(a.position + new Vector2(x + direction.x * multip, y) * multip);
                    if (hit != null)
                        Destroy(hit.gameObject);
                }

                if (y > 0 && y < Mathf.Abs(direction.y) - 1)
                    Instantiate(jumpBoard, a.position + new Vector2(x + direction.x * multip, y) * multip, Quaternion.identity);
            }
        }
    }
}
