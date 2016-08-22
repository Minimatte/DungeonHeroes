using UnityEngine;
using System.Collections;

public class SpawnWorld : MonoBehaviour {

    public GameObject[] firstPiece;

	void Start () {
        Instantiate(firstPiece[Random.Range(0, firstPiece.Length)], new Vector3(0, 0), Quaternion.identity);
	}

}
