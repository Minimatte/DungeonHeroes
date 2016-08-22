using UnityEngine;
using System.Collections;

[System.Serializable]
public class CaveGenerator {

    private bool[,] map;
    [Range(0, 100)]
    public float chanceToStartAlive;
    [Range(0, 10)]
    public int simulationSteps;
    [Range(0, 8)]
    public int deathLimit, birthLimit,treasureHiddenLimit;

    private float width, height;

    public CaveGenerator(int width, int height) {
        map = new bool[width, height];
    }

    public bool[,] InitializaMap(bool[,] map) {
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                if (Random.Range(0, 100) < chanceToStartAlive)
                    map[x, y] = true;
            }
        }

        return map;
    }

    public bool[,] GetMap() {
        return map;
    }

    public bool[,] DoSimulationStep(bool[,] oldMap) {
        bool[,] newMap = new bool[oldMap.GetLength(0), oldMap.GetLength(1)];
        for (int x = 0; x < oldMap.GetLength(0); x++) {
            for (int y = 0; y < oldMap.GetLength(1); y++) {
                int nbs = CountAliveNeighbours(oldMap, x, y);
                if (oldMap[x, y]) {
                    if (nbs < deathLimit) {
                        newMap[x, y] = false;
                    } else {
                        newMap[x, y] = true;
                    }
                } 
                else {
                    if (nbs > birthLimit) {
                        newMap[x, y] = true;
                    } else {
                        newMap[x, y] = false;
                    }
                }
            }
        }
        return newMap;
    }

    public int CountAliveNeighbours(bool[,] map, int x, int y) {
        int count = 0;
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                int neighbour_x = x + i;
                int neighbour_y = y + j;

                if (i == 0 && j == 0) {
                }
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1)) {
                    count = count + 1;
                }
                else if (map[neighbour_x, neighbour_y]) {
                    count = count + 1;
                }
            }
        }
        return count;
    }
}
