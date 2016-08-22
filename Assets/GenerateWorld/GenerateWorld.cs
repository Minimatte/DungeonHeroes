using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour
{

    public float world = 0;

    public GameObject[] worldPieces;
    public GameObject exit;

    public int numberOfWorldPieces = 10;

    void Awake()
    {
        for (int i = 0; i < numberOfWorldPieces; i++)
        {
            Instantiate(worldPieces[Random.Range(0, worldPieces.Length)], new Vector3(i * 3.2f, 0), Quaternion.identity);
        }
        Instantiate(exit, new Vector3((numberOfWorldPieces * 3.2f), 0), Quaternion.identity);
    }
}
