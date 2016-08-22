using UnityEngine;
using System.Collections;

public class DungeonRoom  {

    public int x, y;
    public int width, height;
	
    public DungeonRoom() {

    }

    public DungeonRoom(int x, int y, int width, int height) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public void RandomizeRoomSize(int minWidth, int maxWidth, int minHeight,int maxHeight) {
        System.Random rnd = new System.Random();
        int width = rnd.Next(minWidth, maxWidth);
        int height = rnd.Next(minWidth, maxWidth);

        this.width = width;
        this.height = height;
    }

    public void RandomizePosition(int minX, int maxX, int minY,int maxY) {
        int x = Random.Range(minX, maxX);
        int y = Random.Range(minY, maxY);

        this.x = x;
        this.y = y;

    }

}
