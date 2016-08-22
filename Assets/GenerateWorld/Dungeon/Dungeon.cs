using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

    public GameObject worldTilePrefab;
    public float prefabSize;


    public int tries;

    public DungeonRoom[,] roomGrid;
    bool[,] dungeonGrid;

    public int minimumRoomSize = 5;
    public Vector2 maxRoomSize;
    public int roomsizeX { get { return (int)maxRoomSize.x; } }
    public int roomsizeY { get { return (int)maxRoomSize.y; } }

    public Vector2 gridSize;
    public int gridSizeX { get { return (int)gridSize.x; } }
    public int gridSizeY { get { return (int)gridSize.y; } }

    void Start() {
        
        roomGrid = new DungeonRoom[gridSizeX, gridSizeY];
        dungeonGrid = new bool[gridSizeX, gridSizeY];

        for (int x = 0; x < dungeonGrid.GetLength(0) - 1; x++) {
            for (int y = 0; y < dungeonGrid.GetLength(1) - 1; y++) {
                dungeonGrid[x, y] = true;
            }

        }

   
        PopulateDungeon();
    }

    public void PopulateDungeon() {
        print("starting...");
        AddRooms();
        
        SpawnWorld();
    }

    private void AddRooms() {
        System.Random rnd = new System.Random();
        for (int i = 0; i < tries; i++) {
            print("try " + i);
            DungeonRoom tryRoom = RandomizeRoom(rnd);

            if (DoesRoomFit(tryRoom)) {
                roomGrid[tryRoom.x, tryRoom.y] = tryRoom;
                SetRoom(tryRoom);
            }
        }
    }

    private DungeonRoom RandomizeRoom(System.Random rnd) {
        DungeonRoom room = new DungeonRoom();
        
        int rx = rnd.Next(0, dungeonGrid.GetLength(0));
        int ry = rnd.Next(0, dungeonGrid.GetLength(1));

        room.x = rx;
        room.y = ry;

        room.RandomizeRoomSize(minimumRoomSize, roomsizeX, minimumRoomSize, roomsizeY);

        return room;
    }

    private Vector2 RandomizeRoomLocation() {
        int x, y;
        x = Random.Range(0, gridSizeX);
        y = Random.Range(0, gridSizeY);
        return new Vector2(x, y);
    }


    private void SetRoom(DungeonRoom room) {
        int x = room.x;
        int y = room.y;
        int width = room.width;
        int height = room.height;

        if (x + width > dungeonGrid.GetLength(0)) {
           
            return;
        }
        if (y + height > dungeonGrid.GetLength(1)) {
           
            return;
        }


        for (int i = x; i < x + width; i++) {
            for (int j = y; j < y + height; j++) {
                dungeonGrid[i, j] = false;
            }
        }
    }

    public bool DoesRoomFit(DungeonRoom room) {

        print("looking to see if room fits");
        int x = room.x;
        int y = room.y;
        int width = room.width;
        int height = room.height;

        if (x + width > dungeonGrid.GetLength(0)) {
            
            return false;
        }
        if (y + height > dungeonGrid.GetLength(1)) {
            
            return false;
        }


        for (int i = x; i < x + width; i++) {
            for (int j = y; j < (y + height); j++) {


                if (!dungeonGrid[i, j]) {

                    return false;
                }
            }
        }
        print("Room fits...!");
        return true;
    }


    public void SpawnWorld() {
        print("Spawning the world...");
        for (int x = 0; x < dungeonGrid.GetLength(0); x++) {
            for (int y = 0; y < dungeonGrid.GetLength(1); y++) {
                if (dungeonGrid[x, y]) {
                    Instantiate(worldTilePrefab, new Vector3(x * prefabSize, y * prefabSize, 0), Quaternion.identity);
                }
            }
        }
        print("...Done!");
    }

}