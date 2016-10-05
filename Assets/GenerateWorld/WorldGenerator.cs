using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour {

    public CaveGenerator caveGen;
    public Vector2 mapSize;

    public GameObject tile;
    public float tileSize;

    public GameObject treasure;

    private bool[,] map;

    void Start() {
        map = new bool[(int)mapSize.x, (int)mapSize.y];
        StartCoroutine(spawnWorld());
    }

    void SpawnTiles(bool[,] map) {
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                if (map[x, y]) {
                    GameObject t = Instantiate(tile, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity) as GameObject;
                    t.transform.SetParent(transform, true);
                }
            }
        }
    }

    private IEnumerator spawnWorld() {
        //caveGen = new CaveGenerator((int)mapSize.x, (int)mapSize.y);

        caveGen.InitializaMap(map);

        for (int i = 0; i < caveGen.simulationSteps; i++)
            map = caveGen.DoSimulationStep(map);

        SpawnTiles(map);
        SpawnFrameAroundWorld(map);

        PlaceTreasure(map);
        yield return 0;
    }

    private void SpawnFrameAroundWorld(bool[,] map) {
        for (int x = 0; x < map.GetLength(0); x++) { // bottom of the level
            GameObject t = Instantiate(tile, new Vector3(x * tileSize, -1, 0), Quaternion.identity) as GameObject;
            t.transform.SetParent(transform, true);
        }

        for (int x = 0; x < map.GetLength(0); x++) { // top of the level
            GameObject t = Instantiate(tile, new Vector3(x * tileSize, mapSize.y, 0), Quaternion.identity) as GameObject;
            t.transform.SetParent(transform, true);
        }

        for (int y = 0; y < map.GetLength(1); y++) { // left of the level
            GameObject t = Instantiate(tile, new Vector3(-1, y * tileSize, 0), Quaternion.identity) as GameObject;
            t.transform.SetParent(transform, true);
        }

        for (int y = 0; y < map.GetLength(1); y++) { // right of the level
            GameObject t = Instantiate(tile, new Vector3(mapSize.x, y * tileSize, 0), Quaternion.identity) as GameObject;
            t.transform.SetParent(transform, true);
        }
    }

    public void PlaceTreasure(bool[,] world) {
        //How hidden does a spot need to be for treasure?
        //I find 5 or 6 is good. 6 for very rare treasure.

        for (int x = 0; x < world.GetLength(0); x++) {
            for (int y = 0; y < world.GetLength(1); y++) {
                if (!world[x, y]) {
                    int nbs = caveGen.CountAliveNeighbours(world, x, y);
                    if (nbs >= caveGen.treasureHiddenLimit) {
                        PlaceTreasure(x, y);
                    }
                }
            }
        }
    }

    private void PlaceTreasure(int x, int y) {
        Instantiate(treasure, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);
    }

}
