using UnityEngine;
using System.Collections;

public class SpawnNextTile : MonoBehaviour
{

    public GameObject[] nextTile;
    public static int numTiles = 0;

    public GameObject exit;

    void Start()
    {
        numTiles++;
        if (numTiles < 10)
        {
            int r = Random.Range(0, nextTile.Length);
            Instantiate(nextTile[r], new Vector3(numTiles * 3.2f, 0), Quaternion.identity);
            print(r);
        }
        else
        {
            Instantiate(exit, new Vector3(numTiles * 3.2f, 0), Quaternion.identity);
        }
    }


}
