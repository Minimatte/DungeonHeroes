using UnityEngine;
using System.Collections;

public class VFXEffect : MonoBehaviour {

    public float Lifetime = 3;

    void Awake() {
        Destroy(gameObject, Lifetime);
    }
}
