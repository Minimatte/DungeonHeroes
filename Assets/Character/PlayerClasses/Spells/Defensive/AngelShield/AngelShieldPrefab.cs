using UnityEngine;
using System.Collections;

public class AngelShieldPrefab : MonoBehaviour {

    public Transform owner;

    void Update() {
        transform.position = (Vector2)owner.position + Vector2.up * 0.16f;
    }
}
