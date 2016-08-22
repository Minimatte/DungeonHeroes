using UnityEngine;
using System.Collections;

public class Room {

    public float x, y; // world position

    public int width, height;

    public Room rightRoom, leftRoom;
    public TreeRoomInstance instance;
    TreeDungeon dungeon;


    public Vector2 RandomPositionInRoom {
        get { return bottomLeft + new Vector2(Random.Range(2, width - 2), Random.Range(2, height - 2)); }
    }

    public Vector2 position {
        get { return new Vector2(x + ((width) / 2), y + ((height) / 2)); }
    }

    public Vector2 bottomLeft {
        get { return new Vector2(x, y); }
    }

    public Vector2 topRight {
        get { return new Vector2(x + width, y + height); }
    }

    public Vector2 right {
        get { return new Vector2(x + width, y + height / 2); }
    }

    public Vector2 left {
        get { return new Vector2(x, y + height / 2); }
    }

    public Vector2 up {
        get { return new Vector2(x + width / 2, y + height); }
    }

    public Vector2 down {
        get { return new Vector2(x + width / 2, y); }
    }

    public Vector2 middle {
        get { return new Vector2(x + width / 2, y - 1 + height / 2); }
    }

    public Room(float x, float y, Vector2 roomSizeMin, Vector2 roomSizeMax, TreeDungeon dungeon) {
        this.x = x;
        this.y = y;
        this.width = Random.Range((int)roomSizeMin.x, (int)roomSizeMax.x);
        this.height = Random.Range((int)roomSizeMin.y, (int)roomSizeMax.y);
        this.dungeon = dungeon;
    }

    public Room(float x, float y, int width, int height, TreeDungeon dungeon) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.dungeon = dungeon;
    }

    public Room MakeNeighbours(int minRoomWidth, int minRoomHeight, int maxRoomWidth, int maxRoomHeight, int distanceMax, int distanceMin) {

        while (rightRoom == null || leftRoom == null) {

            Vector2 direction = RandomPointInEllipse(1, 0.1f);

            float distanceAwayX = Random.Range(distanceMin, distanceMax);
            float distanceAwayY = Random.Range(distanceMin, distanceMax);
            int roomWidth = Random.Range(minRoomWidth, maxRoomWidth);
            int roomHeight = Random.Range(minRoomHeight, maxRoomHeight);

            Vector2 topLeft = new Vector2(x + distanceAwayX * direction.x, y + distanceAwayY * direction.y);
            Vector2 bottomRight = new Vector2(x + distanceAwayX * direction.x + roomWidth, y + distanceAwayY * direction.y + roomHeight);

            if (Physics2D.OverlapArea(topLeft, bottomRight) != null) {
                continue;
            }

            if (rightRoom == null) {
                rightRoom = new Room(Mathf.FloorToInt(x + distanceAwayX * direction.x), Mathf.FloorToInt(y + distanceAwayY * direction.y), roomWidth, roomHeight, dungeon);
                return rightRoom;
            } else {
                leftRoom = new Room(Mathf.FloorToInt(x + distanceAwayX * direction.x), Mathf.FloorToInt(y + distanceAwayY * direction.y), roomWidth, roomHeight, dungeon);
                return leftRoom;
            }
        }
        return null;
    }

    public override string ToString() {
        return "Room " + x + "," + y;
    }

    private Vector2 RandomPointInEllipse(float distanceX, float distanceY) {

        var x = Random.Range(-distanceX, distanceX);
        var y = Random.Range(-distanceY, distanceY);

        Vector2 ellipse = new Vector2(x, y).normalized;

        return ellipse;
    }

}
